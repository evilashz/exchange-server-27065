using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02001178 RID: 4472
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewPolicyTipConfigInvalidLocaleException : LocalizedException
	{
		// Token: 0x0600B641 RID: 46657 RVA: 0x0029F7F5 File Offset: 0x0029D9F5
		public NewPolicyTipConfigInvalidLocaleException(string locales) : base(Strings.NewPolicyTipConfigInvalidLocale(locales))
		{
			this.locales = locales;
		}

		// Token: 0x0600B642 RID: 46658 RVA: 0x0029F80A File Offset: 0x0029DA0A
		public NewPolicyTipConfigInvalidLocaleException(string locales, Exception innerException) : base(Strings.NewPolicyTipConfigInvalidLocale(locales), innerException)
		{
			this.locales = locales;
		}

		// Token: 0x0600B643 RID: 46659 RVA: 0x0029F820 File Offset: 0x0029DA20
		protected NewPolicyTipConfigInvalidLocaleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.locales = (string)info.GetValue("locales", typeof(string));
		}

		// Token: 0x0600B644 RID: 46660 RVA: 0x0029F84A File Offset: 0x0029DA4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("locales", this.locales);
		}

		// Token: 0x1700397E RID: 14718
		// (get) Token: 0x0600B645 RID: 46661 RVA: 0x0029F865 File Offset: 0x0029DA65
		public string Locales
		{
			get
			{
				return this.locales;
			}
		}

		// Token: 0x040062E4 RID: 25316
		private readonly string locales;
	}
}
