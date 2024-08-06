using TonProof.Types;

namespace TonProof.Tests.Data;

public class CheckProofRequestV5Testnet: TheoryData<CheckProofRequest>
{
    public CheckProofRequestV5Testnet()
    {
        this.Add(new CheckProofRequest
        {
            Address = "0:7afd7563d8fd084d35060278320769743905983b8ea43c5b16fb6e3dc0a190dd",
            Network = "-3",
            PublicKey = "c5134fcb39879c2cf81ffdc347353d031cf184538130fdd42688152088bf69ba",
            Proof = new Proof
            {
                Timestamp = 1721812530,
                Domain = new Domain
                {
                    LengthBytes = 20,
                    Value = "winogarcia.github.io"
                },
                Signature = "8ab+IBU83nga6aNznUNEHr3OgN1L7I2raVomWNrhEElTFI1a3VvAz4mzvQbaYiaCY+WftHZrJnSiEgnbRYojDA==",
                Payload = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQxNDA1NzUxLTNlZjEtNDZhYi04ZDJiLTRkMjE4NWVhZjI2ZSIsIm5iZiI6MTcyMTgxMjQ4OCwiZXhwIjoxNzIxODEzMzg4LCJpYXQiOjE3MjE4MTI0ODgsImlzcyI6Iklzc3VlckZvclBheWxvYWQiLCJhdWQiOiJBdWRpZW5jZUZvclBheWxvYWQifQ.rzJ1tpq-jDp19pDwWO8ZNp8T9ZkME9CZXIppWc5FYGo",
                StateInit = "te6cckEBAwEAWQACATQCAQBdAAAAAH////6AAAAAAABiiaflnMPOFnwP/uGjmp6BjnjCKcCYfuoTRAqQRF+03SAIQgLkzzsvTG1qYeoPK1RH0mZ4WyavNjfbLe7mvNGqgm80EpjwGJs="
            }
        });
    }
}