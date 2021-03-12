using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class ComputerIdParameter : UserContactComputerIdParameter
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x00028033 File Offset: 0x00026233
		public ComputerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002803C File Offset: 0x0002623C
		public ComputerIdParameter()
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00028044 File Offset: 0x00026244
		public ComputerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002804D File Offset: 0x0002624D
		public ComputerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00028056 File Offset: 0x00026256
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return ComputerIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002805D File Offset: 0x0002625D
		public new static ComputerIdParameter Parse(string identity)
		{
			return new ComputerIdParameter(identity);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00028065 File Offset: 0x00026265
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeComputer(id);
		}

		// Token: 0x040002E7 RID: 743
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Computer
		};
	}
}
