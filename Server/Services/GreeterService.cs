using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Weather;
using System.Linq;
using Rippled;
using Google.Protobuf;

namespace Server
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }

    public class RippledService : XRPLedgerAPIService.XRPLedgerAPIServiceBase
    {
        private readonly ILogger<RippledService> _logger;

        public RippledService(ILogger<RippledService> logger)
        {
            _logger = logger;
        }

        public override Task<GetAccountInfoResponse> GetAccountInfo(GetAccountInfoRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetAccountInfoResponse
            {
                AccountData = new AccountRoot()
                {
                    Account = new Account()
                    {
                        Value = new AccountAddress()
                        {
                            Address = ""
                        }
                    },
                    AccountTransactionId = new AccountTransactionID()
                    {
                        Value = Google.Protobuf.ByteString.CopyFromUtf8("")
                    },
                    Balance = new Balance()
                    {
                        Value = new CurrencyAmount()
                        {
                            //AmountCase = CurrencyAmount.AmountOneofCase.XrpAmount, 
                            IssuedCurrencyAmount = new IssuedCurrencyAmount()
                            {
                                Currency = new Currency
                                {
                                    Code = Google.Protobuf.ByteString.CopyFromUtf8("xrp"),
                                    Name = "XRP"
                                },
                                Issuer = new AccountAddress()
                                {
                                    Address = "",
                                },
                                Value = ""
                            },
                            XrpAmount = new XRPDropsAmount() { Drops = 0ul },
                        }
                    },
                    Domain = new Domain()
                    {
                        Value = ""
                    },
                    EmailHash = new EmailHash()
                    {
                        Value = ByteString.CopyFromUtf8("")
                    },
                    Flags = new Flags()
                    {
                        Value = 0
                    },
                    MessageKey = new MessageKey()
                    {
                        Value = ByteString.CopyFromUtf8("")
                    },
                    OwnerCount = new OwnerCount()
                    {
                        Value = 0
                    },
                    PreviousTransactionId = new PreviousTransactionID()
                    {
                        Value = ByteString.CopyFromUtf8("")
                    },
                    PreviousTransactionLedgerSequence = new PreviousTransactionLedgerSequence()
                    {
                        Value = 0
                    },
                    RegularKey = new RegularKey()
                    {
                        Value = new AccountAddress()
                        {
                            Address = ""
                        }
                    },
                    Sequence = new Sequence()
                    {
                        Value = 0,
                    },
                    TickSize = new TickSize()
                    {
                        Value = 0
                    },
                    TransferRate = new TransferRate()
                    {
                        Value = 0
                    },
                },
                LedgerIndex = 0,
                QueueData = new QueueData()
                {
                    AuthChangeQueued = false,
                    HighestSequence = 0,
                    LowestSequence = 0,
                    MaxSpendDropsTotal = new XRPDropsAmount() { Drops = 0 },
                    //Transactions,
                    TxnCount = 0
                },
                SignerList = new SignerList()
                {
                    Flags = new Flags() { Value = 0 },
                    OwnerNode = new OwnerNode() { Value = 0 },
                    PreviousTransactionId = new PreviousTransactionID()
                    {
                        Value = ByteString.CopyFromUtf8("")
                    },
                    PreviousTransactionLedgerSequence = new PreviousTransactionLedgerSequence()
                    {
                        Value = 0
                    },
                    //SignerEntries,
                    SignerListId = new SignerListID()
                    {
                        Value = 0
                    },
                    SignerQuorum = new SignerQuorum()
                    {
                        Value = 0
                    }
                },
                Validated = false,
            });
        }
    }
}
