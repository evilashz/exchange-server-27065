using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02001202 RID: 4610
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CallAnsweringRuleNotFoundException : LocalizedException
	{
		// Token: 0x0600B9E0 RID: 47584 RVA: 0x002A6853 File Offset: 0x002A4A53
		public CallAnsweringRuleNotFoundException(string identity) : base(Strings.CallAnsweringRuleNotFoundException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600B9E1 RID: 47585 RVA: 0x002A6868 File Offset: 0x002A4A68
		public CallAnsweringRuleNotFoundException(string identity, Exception innerException) : base(Strings.CallAnsweringRuleNotFoundException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600B9E2 RID: 47586 RVA: 0x002A687E File Offset: 0x002A4A7E
		protected CallAnsweringRuleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600B9E3 RID: 47587 RVA: 0x002A68A8 File Offset: 0x002A4AA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17003A55 RID: 14933
		// (get) Token: 0x0600B9E4 RID: 47588 RVA: 0x002A68C3 File Offset: 0x002A4AC3
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006470 RID: 25712
		private readonly string identity;
	}
}
