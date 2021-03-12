using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F9 RID: 1785
	[Serializable]
	public class ADDomainController : ADPresentationObject
	{
		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x00131473 File Offset: 0x0012F673
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ADDomainController.schema;
			}
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0013147A File Offset: 0x0012F67A
		public ADDomainController()
		{
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x00131482 File Offset: 0x0012F682
		public ADDomainController(ADServer dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x0013148B File Offset: 0x0012F68B
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x0013149D File Offset: 0x0012F69D
		public string DnsHostName
		{
			get
			{
				return (string)this[ADServerSchema.DnsHostName];
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x001314AF File Offset: 0x0012F6AF
		public ADObjectId ADSite
		{
			get
			{
				return ((ADServer)base.DataObject).Site;
			}
		}

		// Token: 0x0400387D RID: 14461
		private static ADDomainControllerSchema schema = ObjectSchema.GetInstance<ADDomainControllerSchema>();
	}
}
