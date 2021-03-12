using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002CC RID: 716
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsOutofDatabaseScopeException : LocalizedException
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x0005D11D File Offset: 0x0005B31D
		public IsOutofDatabaseScopeException(string id, string exceptionDetails) : base(Strings.ErrorIsOutofDatabaseScope(id, exceptionDetails))
		{
			this.id = id;
			this.exceptionDetails = exceptionDetails;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005D13A File Offset: 0x0005B33A
		public IsOutofDatabaseScopeException(string id, string exceptionDetails, Exception innerException) : base(Strings.ErrorIsOutofDatabaseScope(id, exceptionDetails), innerException)
		{
			this.id = id;
			this.exceptionDetails = exceptionDetails;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0005D158 File Offset: 0x0005B358
		protected IsOutofDatabaseScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
			this.exceptionDetails = (string)info.GetValue("exceptionDetails", typeof(string));
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0005D1AD File Offset: 0x0005B3AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
			info.AddValue("exceptionDetails", this.exceptionDetails);
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0005D1D9 File Offset: 0x0005B3D9
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0005D1E1 File Offset: 0x0005B3E1
		public string ExceptionDetails
		{
			get
			{
				return this.exceptionDetails;
			}
		}

		// Token: 0x0400099A RID: 2458
		private readonly string id;

		// Token: 0x0400099B RID: 2459
		private readonly string exceptionDetails;
	}
}
