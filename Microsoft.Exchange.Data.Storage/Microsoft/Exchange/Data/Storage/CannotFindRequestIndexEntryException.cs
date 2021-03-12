using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000131 RID: 305
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindRequestIndexEntryException : StoragePermanentException
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x0006B095 File Offset: 0x00069295
		public CannotFindRequestIndexEntryException(Guid requestGuid) : base(ServerStrings.CannotFindRequestIndexEntry(requestGuid))
		{
			this.requestGuid = requestGuid;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0006B0AA File Offset: 0x000692AA
		public CannotFindRequestIndexEntryException(Guid requestGuid, Exception innerException) : base(ServerStrings.CannotFindRequestIndexEntry(requestGuid), innerException)
		{
			this.requestGuid = requestGuid;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0006B0C0 File Offset: 0x000692C0
		protected CannotFindRequestIndexEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestGuid = (Guid)info.GetValue("requestGuid", typeof(Guid));
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0006B0EA File Offset: 0x000692EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestGuid", this.requestGuid);
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0006B10A File Offset: 0x0006930A
		public Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
		}

		// Token: 0x040009C5 RID: 2501
		private readonly Guid requestGuid;
	}
}
