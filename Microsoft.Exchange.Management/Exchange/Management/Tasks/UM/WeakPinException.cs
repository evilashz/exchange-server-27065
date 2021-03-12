using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B3 RID: 4531
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WeakPinException : LocalizedException
	{
		// Token: 0x0600B869 RID: 47209 RVA: 0x002A48A4 File Offset: 0x002A2AA4
		public WeakPinException(string details) : base(Strings.ErrorWeakPassword(details))
		{
			this.details = details;
		}

		// Token: 0x0600B86A RID: 47210 RVA: 0x002A48B9 File Offset: 0x002A2AB9
		public WeakPinException(string details, Exception innerException) : base(Strings.ErrorWeakPassword(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x0600B86B RID: 47211 RVA: 0x002A48CF File Offset: 0x002A2ACF
		protected WeakPinException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x0600B86C RID: 47212 RVA: 0x002A48F9 File Offset: 0x002A2AF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x17003A1A RID: 14874
		// (get) Token: 0x0600B86D RID: 47213 RVA: 0x002A4914 File Offset: 0x002A2B14
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04006435 RID: 25653
		private readonly string details;
	}
}
