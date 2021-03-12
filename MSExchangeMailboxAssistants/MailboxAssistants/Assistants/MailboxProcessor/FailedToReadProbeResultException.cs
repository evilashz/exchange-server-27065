using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor
{
	// Token: 0x0200014B RID: 331
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToReadProbeResultException : LocalizedException
	{
		// Token: 0x06000D48 RID: 3400 RVA: 0x000521E3 File Offset: 0x000503E3
		public FailedToReadProbeResultException(int stateAttribute, string lookingFor, string actualValue) : base(Strings.descFailedToReadProbeResult(stateAttribute, lookingFor, actualValue))
		{
			this.stateAttribute = stateAttribute;
			this.lookingFor = lookingFor;
			this.actualValue = actualValue;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00052208 File Offset: 0x00050408
		public FailedToReadProbeResultException(int stateAttribute, string lookingFor, string actualValue, Exception innerException) : base(Strings.descFailedToReadProbeResult(stateAttribute, lookingFor, actualValue), innerException)
		{
			this.stateAttribute = stateAttribute;
			this.lookingFor = lookingFor;
			this.actualValue = actualValue;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00052230 File Offset: 0x00050430
		protected FailedToReadProbeResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.stateAttribute = (int)info.GetValue("stateAttribute", typeof(int));
			this.lookingFor = (string)info.GetValue("lookingFor", typeof(string));
			this.actualValue = (string)info.GetValue("actualValue", typeof(string));
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000522A5 File Offset: 0x000504A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("stateAttribute", this.stateAttribute);
			info.AddValue("lookingFor", this.lookingFor);
			info.AddValue("actualValue", this.actualValue);
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000522E2 File Offset: 0x000504E2
		public int StateAttribute
		{
			get
			{
				return this.stateAttribute;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x000522EA File Offset: 0x000504EA
		public string LookingFor
		{
			get
			{
				return this.lookingFor;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000522F2 File Offset: 0x000504F2
		public string ActualValue
		{
			get
			{
				return this.actualValue;
			}
		}

		// Token: 0x04000842 RID: 2114
		private readonly int stateAttribute;

		// Token: 0x04000843 RID: 2115
		private readonly string lookingFor;

		// Token: 0x04000844 RID: 2116
		private readonly string actualValue;
	}
}
