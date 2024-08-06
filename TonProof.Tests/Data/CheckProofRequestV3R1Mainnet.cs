using TonProof.Types;

namespace TonProof.Tests.Data;

public class CheckProofRequestV3R1Mainnet : TheoryData<CheckProofRequest>
{
    public CheckProofRequestV3R1Mainnet()
    {
        this.Add(new CheckProofRequest
        {
            Address = "0:c83546fdc5fac87d55f04804919c0829bc0e6be24b8e564590dbc44380c30ee9",
            Network = "-239",
            PublicKey = "e635249f6b3fbaf9742ae1d09d8f6f01b6082ff9256d719006329405019f3198",
            Proof = new Proof
            {
                Timestamp = 1721827131,
                Domain = new Domain
                {
                    LengthBytes = 20,
                    Value = "winogarcia.github.io"
                },
                Signature = "LVQzfrfwz8I5ZCl7WFpGdICvYbHxPplZnMKx0gT/X0jFlDOohPa9/6RtzvcSGWqtHBwE6XlUUadmcZoTMlyUAw==",
                Payload = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjExMjM3N2UxLWUwYjktNGIyOC05NjJkLTRiZDAxMTFmZmY3NyIsIm5iZiI6MTcyMTgyNzA4MiwiZXhwIjoxNzIxODI3OTgyLCJpYXQiOjE3MjE4MjcwODIsImlzcyI6Iklzc3VlckZvclBheWxvYWQiLCJhdWQiOiJBdWRpZW5jZUZvclBheWxvYWQifQ.BZ6LvTKvsSPZ4H6nbwcQP3gjVgvkbYm2vPIQjJ4Gyak",
                StateInit = "te6cckEBAwEAkQACATQCAQBQAAAAACmpoxfmNSSfaz+6+XQq4dCdj28Btggv+SVtcZAGMpQFAZ8xmADA/wAg3SCCAUyXupcw7UTQ1wsf4KTyYIMI1xgg0x/TH9Mf+CMTu/Jj7UTQ0x/TH9P/0VEyuvKhUUS68qIE+QFUEFX5EPKj+ACTINdKltMH1AL7AOjRAaTIyx/LH8v/ye1UGy3yHQ=="
            }
        });
    }
}