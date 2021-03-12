using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RuleInvalidOperationException : LocalizedException
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000A972 File Offset: 0x00008B72
		public RuleInvalidOperationException(string details) : base(RulesStrings.RuleInvalidOperationDescription(details))
		{
			this.details = details;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A987 File Offset: 0x00008B87
		public RuleInvalidOperationException(string details, Exception innerException) : base(RulesStrings.RuleInvalidOperationDescription(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A99D File Offset: 0x00008B9D
		protected RuleInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A9E2 File Offset: 0x00008BE2
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04000142 RID: 322
		private readonly string details;
	}
}
