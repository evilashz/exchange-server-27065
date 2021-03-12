using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035C RID: 860
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropertyTagsDoNotMatchPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002689 RID: 9865 RVA: 0x00053479 File Offset: 0x00051679
		public PropertyTagsDoNotMatchPermanentException(uint propTagFromSource, uint propTagFromDestination) : base(MrsStrings.PropertyTagsDoNotMatch(propTagFromSource, propTagFromDestination))
		{
			this.propTagFromSource = propTagFromSource;
			this.propTagFromDestination = propTagFromDestination;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00053496 File Offset: 0x00051696
		public PropertyTagsDoNotMatchPermanentException(uint propTagFromSource, uint propTagFromDestination, Exception innerException) : base(MrsStrings.PropertyTagsDoNotMatch(propTagFromSource, propTagFromDestination), innerException)
		{
			this.propTagFromSource = propTagFromSource;
			this.propTagFromDestination = propTagFromDestination;
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000534B4 File Offset: 0x000516B4
		protected PropertyTagsDoNotMatchPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propTagFromSource = (uint)info.GetValue("propTagFromSource", typeof(uint));
			this.propTagFromDestination = (uint)info.GetValue("propTagFromDestination", typeof(uint));
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x00053509 File Offset: 0x00051709
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propTagFromSource", this.propTagFromSource);
			info.AddValue("propTagFromDestination", this.propTagFromDestination);
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x00053535 File Offset: 0x00051735
		public uint PropTagFromSource
		{
			get
			{
				return this.propTagFromSource;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x0005353D File Offset: 0x0005173D
		public uint PropTagFromDestination
		{
			get
			{
				return this.propTagFromDestination;
			}
		}

		// Token: 0x04001062 RID: 4194
		private readonly uint propTagFromSource;

		// Token: 0x04001063 RID: 4195
		private readonly uint propTagFromDestination;
	}
}
