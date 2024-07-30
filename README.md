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
***REMOVED***
  o.ValidAuthTime = 30 * 60; // 30 minutes
  o.AllowedDomains = new[] ***REMOVED*** "example.com" ***REMOVED***;
  // Add other known wallet contracts...
  // o.KnownWallets.Add(WalletContractV5.CodeBase64, WalletContractV5.Create);
***REMOVED***);

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
***REMOVED***
  var payload = this.CreatePayloadToken();

  var response = new GeneratePayloadResponse()
  ***REMOVED***
      Payload = payload
  ***REMOVED***;
  return this.Ok(response);
***REMOVED***
```
<details>
<summary>Response</summary>
<pre>
***REMOVED***
  "payload": "string"
***REMOVED***
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
  -d '***REMOVED***
  "address": "0:13f04fa2a978c...",
  "network": "-3",
  "public_key": "c5134fcb...",
  "proof": ***REMOVED***
    "timestamp": 1721812530,
    "domain": ***REMOVED***
      "LengthBytes": 20,
      "value": "winogarcia.github.io"
***REMOVED***
    "signature": "YlhjR9vEhyGyYbr...",
    "payload": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
    "state_init": "te6cc..."
  ***REMOVED***
***REMOVED***'
</pre>
</details>

```csharp
[AllowAnonymous]
public async Task<ActionResult<CheckProofResponse>> CheckProof(CheckProofRequest request, CancellationToken cancellationToken)
***REMOVED***
  var verifyResult = await this.TonProofService.VerifyAsync(request, cancellationToken);
  if (verifyResult != VerifyResult.Valid)
  ***REMOVED***
    return this.BadRequest($"Invalid proof: ***REMOVED***Enum.GetName(verifyResult)***REMOVED***");
  ***REMOVED***

  var payload = request.Proof.Payload;
  if (!this.ValidateToken(payload))
  ***REMOVED***
    return this.BadRequest("Invalid payload");
  ***REMOVED***

  var token = this.CreateToken(payload, request.Address);

  return this.Ok(new CheckProofResponse()
  ***REMOVED***
    Token = token
  ***REMOVED***);
***REMOVED***
```
<details>
<summary>Response</summary>
<pre>
***REMOVED***
  "token": "string"
***REMOVED***
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
***REMOVED***
  await this.tonClient.InitIfNeededAsync(cancellationToken);

  var address = this.GetUserAddress();
  var ast = await this.tonClient.GetAccountState(address);

  var response = new BalanceResponse
  ***REMOVED***
    Amount = ast.Balance.ToString()
  ***REMOVED***;
  return this.Ok(response);
***REMOVED***
```
<details>
<summary>Response</summary>
<pre>
***REMOVED***
  "amount": "777777"
***REMOVED***
</pre>
</details>

For more information, please refer to the complete example [AccountController.cs](/TonProof.Demo/AccountController.cs)

More information is available in the demo project [TonProof.Demo](/TonProof.Demo)

## 3rd-party libraries and dependencies
- [TonLib.Net](https://github.com/justdmitry/TonLib.NET)
- [NSec.Cryptography](https://nsec.rocks/)