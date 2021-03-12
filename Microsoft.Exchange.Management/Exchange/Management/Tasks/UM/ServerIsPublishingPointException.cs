using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E4 RID: 4580
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerIsPublishingPointException : LocalizedException
	{
		// Token: 0x0600B955 RID: 47445 RVA: 0x002A5D39 File Offset: 0x002A3F39
		public ServerIsPublishingPointException(string dialPlan) : base(Strings.ServerIsPublishingPointException(dialPlan))
		{
			this.dialPlan = dialPlan;
		}

		// Token: 0x0600B956 RID: 47446 RVA: 0x002A5D4E File Offset: 0x002A3F4E
		public ServerIsPublishingPointException(string dialPlan, Exception innerException) : base(Strings.ServerIsPublishingPointException(dialPlan), innerException)
		{
			this.dialPlan = dialPlan;
		}

		// Token: 0x0600B957 RID: 47447 RVA: 0x002A5D64 File Offset: 0x002A3F64
		protected ServerIsPublishingPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dialPlan = (string)info.GetValue("dialPlan", typeof(string));
		}

		// Token: 0x0600B958 RID: 47448 RVA: 0x002A5D8E File Offset: 0x002A3F8E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dialPlan", this.dialPlan);
		}

		// Token: 0x17003A42 RID: 14914
		// (get) Token: 0x0600B959 RID: 47449 RVA: 0x002A5DA9 File Offset: 0x002A3FA9
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x0400645D RID: 25693
		private readonly string dialPlan;
	}
}
