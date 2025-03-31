using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public interface ICreditAssignerSelector
    {
        ICreditAssigner GetAssignType(Client client);
    }

    public class CreditAssignerSelector: ICreditAssignerSelector
    {
        public ICreditAssigner GetAssignType(Client client)
        {
            return client.Name switch
            {
                "VIP" => new VipCreditAssigner(),
                "Important" => new ImportantCreditAssigner(),
                _ => new DefaultCreditAssigner()
            };
        }
    }
}
