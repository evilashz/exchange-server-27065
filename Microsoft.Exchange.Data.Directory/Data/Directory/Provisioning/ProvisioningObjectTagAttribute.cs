using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200079A RID: 1946
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ProvisioningObjectTagAttribute : Attribute
	{
		// Token: 0x060060F4 RID: 24820 RVA: 0x001497F1 File Offset: 0x001479F1
		public ProvisioningObjectTagAttribute(string tag)
		{
			if (string.IsNullOrEmpty(tag))
			{
				throw new ArgumentNullException(tag);
			}
			this.tag = tag;
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x060060F5 RID: 24821 RVA: 0x0014980F File Offset: 0x00147A0F
		public string Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x040040FE RID: 16638
		private string tag;
	}
}
