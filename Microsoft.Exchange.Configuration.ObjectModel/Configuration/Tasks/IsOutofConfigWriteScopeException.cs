using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002CB RID: 715
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsOutofConfigWriteScopeException : LocalizedException
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x0005D051 File Offset: 0x0005B251
		public IsOutofConfigWriteScopeException(string type, string id) : base(Strings.ErrorIsOutofConfigWriteScope(type, id))
		{
			this.type = type;
			this.id = id;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005D06E File Offset: 0x0005B26E
		public IsOutofConfigWriteScopeException(string type, string id, Exception innerException) : base(Strings.ErrorIsOutofConfigWriteScope(type, id), innerException)
		{
			this.type = type;
			this.id = id;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005D08C File Offset: 0x0005B28C
		protected IsOutofConfigWriteScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005D0E1 File Offset: 0x0005B2E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
			info.AddValue("id", this.id);
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0005D10D File Offset: 0x0005B30D
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0005D115 File Offset: 0x0005B315
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000998 RID: 2456
		private readonly string type;

		// Token: 0x04000999 RID: 2457
		private readonly string id;
	}
}
