using TonProof.Types;

namespace TonProof.Tests.Data;

public class CheckProofRequestV4R2Testnet : TheoryData<CheckProofRequest>
{
    public CheckProofRequestV4R2Testnet()
    {
        this.Add(new CheckProofRequest
        {
            Address = "0:13f04fa2a978c6eaccc5c0521c44d87b5be7f673ff20176a0203d90e8b90a7c1",
            Network = "-3",
            PublicKey = "c5134fcb39879c2cf81ffdc347353d031cf184538130fdd42688152088bf69ba",
            Proof = new Proof
            {
                Timestamp = 1718913764,
                Domain = new Domain
                {
                    LengthBytes = 20,
                    Value = "winogarcia.github.io"
                },
                Signature = "YlhjR9vEhyGyYbrGavBFMxvQ0Gj0x3ESHO2mZ/QzMSItfpTw2q52TOkLklpx5VJochzAbkyVmYAvGmHgHcasAg==",
                Payload = "mypayload",
                StateInit = "te6cckECFgEAAwQAAgE0ARUBFP8A9KQT9LzyyAsCAgEgAxACAUgEBwLm0AHQ0wMhcbCSXwTgItdJwSCSXwTgAtMfIYIQcGx1Z70ighBkc3RyvbCSXwXgA/pAMCD6RAHIygfL/8nQ7UTQgQFA1yH0BDBcgQEI9ApvoTGzkl8H4AXTP8glghBwbHVnupI4MOMNA4IQZHN0crqSXwbjDQUGAHgB+gD0BDD4J28iMFAKoSG+8uBQghBwbHVngx6xcIAYUATLBSbPFlj6Ahn0AMtpF8sfUmDLPyDJgED7AAYAilAEgQEI9Fkw7UTQgQFA1yDIAc8W9ADJ7VQBcrCOI4IQZHN0coMesXCAGFAFywVQA88WI/oCE8tqyx/LP8mAQPsAkl8D4gIBIAgPAgEgCQ4CAVgKCwA9sp37UTQgQFA1yH0BDACyMoHy//J0AGBAQj0Cm+hMYAIBIAwNABmtznaiaEAga5Drhf/AABmvHfaiaEAQa5DrhY/AABG4yX7UTQ1wsfgAWb0kK29qJoQICga5D6AhhHDUCAhHpJN9KZEM5pA+n/mDeBKAG3gQFImHFZ8xhAT48oMI1xgg0x/TH9MfAvgju/Jk7UTQ0x/TH9P/9ATRUUO68qFRUbryogX5AVQQZPkQ8qP4ACSkyMsfUkDLH1Iwy/9SEPQAye1U+A8B0wchwACfbFGTINdKltMH1AL7AOgw4CHAAeMAIcAC4wABwAORMOMNA6TIyx8Syx/L/xESExQAbtIH+gDU1CL5AAXIygcVy//J0Hd0gBjIywXLAiLPFlAF+gIUy2sSzMzJc/sAyEAUgQEI9FHypwIAcIEBCNcY+gDTP8hUIEeBAQj0UfKnghBub3RlcHSAGMjLBcsCUAbPFlAE+gIUy2oSyx/LP8lz+wACAGyBAQjXGPoA0z8wUiSBAQj0WfKnghBkc3RycHSAGMjLBcsCUAXPFlAD+gITy2rLHxLLP8lz+wAACvQAye1UAFEAAAAAKamjF8UTT8s5h5ws+B/9w0c1PQMc8YRTgTD91CaIFSCIv2m6QFpYiqg="
            }
        });
    }
}