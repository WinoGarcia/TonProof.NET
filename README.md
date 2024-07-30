TonProof.NET for TON Connect
===========

This repository provides the [TonProofService](/TonProof/TonProofService.cs) for verifying proof requests and handling authentication processes in a [TON Connect](https://docs.ton.org/develop/dapps/ton-connect/overview)-based application. It is specifically designed for ASP.NET Core applications and makes use of the `tonClient` from the [TonLib.Net](https://github.com/justdmitry/TonLib.NET) repository.

## Overview

`ITonProofService` is essential for integrating [TON Connect](https://docs.ton.org/develop/dapps/ton-connect/sign) authentication into your ASP.NET Core application. It validates proof requests by checking the integrity and authenticity of the provided data, including public keys, addresses, domain restrictions, and proof expiration.

## Usage
### Configuration

⚠ Be sure to configure the `tonClient` from the [TonLib.NET](https://github.com/justdmitry/TonLib.NET) repository!

Set up the [TonProofOptions](/TonProof/TonProofOptions.cs) with appropriate values for your TON Connect application.

Example configuration:
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TonProofOptions>(o =>
{
  o.ValidAuthTime = 30 * 60; // 30 minutes
  o.AllowedDomains = new[] { "example.com" };
  // Add other known wallet contracts...
  // o.KnownWallets.Add(WalletContractV5.CodeBase64, WalletContractV5.Create);
});

builder.Services.AddSingleton<ITonClient, TonClient>();
builder.Services.AddSingleton<ITonProofService, TonProofService>();

var app = builder.Build();
```
For more information, please refer to the complete example [TonProof.Demo/Program.cs](/TonProof.Demo/Program.cs)

### Using the service for authentication
To fully implement authentication using `ITonProofService`, you'll need two endpoints: one to generate a payload for the client and another to verify the proof and generate a JWT token (e.g.).

**1. Generating a payload.**  
The client first requests a payload, which will be used in the proof verification step. In this example, the payload is a token that can include any necessary information for the verification process.

Example:
<details>
<summary>Request</summary>
<pre>
url -X 'POST' \
  'https://host/Auth/GeneratePayload' \
  -H 'accept: application/json' \
  -d ''
</pre>
</details>

```csharp
[AllowAnonymous]
public ActionResult<GeneratePayloadResponse> GeneratePayload()
{
  var payload = this.CreatePayloadToken();

  var response = new GeneratePayloadResponse()
  {
      Payload = payload
  };
  return this.Ok(response);
}
```
<details>
<summary>Response</summary>
<pre>
{
  "payload": "string"
}
</pre>
</details>

**2. Verifying the proof and generating a token**.  
After the client receives the payload, they will submit it along with the evidence for verification. The server will then verify the proof and generate a JWT token if the evidence for verification is valid.

Example:
<details>
<summary>Request</summary>
<pre>
curl -X 'POST' \
  'https://host/Auth/CheckProof' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
  "address": "0:13f04fa2a978c...",
  "network": "-3",
  "public_key": "c5134fcb...",
  "proof": {
    "timestamp": 1721812530,
    "domain": {
      "LengthBytes": 20,
      "value": "winogarcia.github.io"
    },
    "signature": "YlhjR9vEhyGyYbr...",
    "payload": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
    "state_init": "te6cc..."
  }
}'
</pre>
</details>

```csharp
[AllowAnonymous]
public async Task<ActionResult<CheckProofResponse>> CheckProof(CheckProofRequest request, CancellationToken cancellationToken)
{
  var verifyResult = await this.TonProofService.VerifyAsync(request, cancellationToken);
  if (verifyResult != VerifyResult.Valid)
  {
    return this.BadRequest($"Invalid proof: {Enum.GetName(verifyResult)}");
  }

  var payload = request.Proof.Payload;
  if (!this.ValidateToken(payload))
  {
    return this.BadRequest("Invalid payload");
  }

  var token = this.CreateToken(payload, request.Address);

  return this.Ok(new CheckProofResponse()
  {
    Token = token
  });
}
```
<details>
<summary>Response</summary>
<pre>
{
  "token": "string"
}
</pre>
</details>

For more information, please refer to the complete example [AuthController.cs](/TonProof.Demo/AuthController.cs)

### Example of an authorized request to get a balance
In this example, we will show how to make an authorized request to retrieve the balance using a JWT token. The JWT token, created in the earlier steps, is employed to authorize the request. The ASP.NET Core middleware verifies the validity of the JWT token, ensuring that only authenticated users can access the endpoint.

<details>
<summary>Request</summary>
<pre>
curl -X 'POST' \
  'https://host/Account/GetBalance' \
  -H 'accept: application/json' \
  -H 'Authorization: Bearer XXXXXXX' \
  -d ''
</pre>
</details>

```csharp
[Authorize]
public async Task<ActionResult<BalanceResponse>> GetBalance(CancellationToken cancellationToken)
{
  await this.tonClient.InitIfNeededAsync(cancellationToken);

  var address = this.GetUserAddress();
  var ast = await this.tonClient.GetAccountState(address);

  var response = new BalanceResponse
  {
    Amount = ast.Balance.ToString()
  };
  return this.Ok(response);
}
```
<details>
<summary>Response</summary>
<pre>
{
  "amount": "777777"
}
</pre>
</details>

For more information, please refer to the complete example [AccountController.cs](/TonProof.Demo/AccountController.cs)

More information is available in the demo project [TonProof.Demo](/TonProof.Demo)

## 3rd-party libraries and dependencies
- [TonLib.Net](https://github.com/justdmitry/TonLib.NET)
- [NSec.Cryptography](https://nsec.rocks/)