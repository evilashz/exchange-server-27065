using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002CD RID: 717
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoServersForDatabaseException : LocalizedException
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x0005D1E9 File Offset: 0x0005B3E9
		public NoServersForDatabaseException(string id) : base(Strings.ErrorNoServersForDatabase(id))
		{
			this.id = id;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005D1FE File Offset: 0x0005B3FE
		public NoServersForDatabaseException(string id, Exception innerException) : base(Strings.ErrorNoServersForDatabase(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0005D214 File Offset: 0x0005B414
		protected NoServersForDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0005D23E File Offset: 0x0005B43E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005D259 File Offset: 0x0005B459
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0400099C RID: 2460
		private readonly string id;
	}
}
