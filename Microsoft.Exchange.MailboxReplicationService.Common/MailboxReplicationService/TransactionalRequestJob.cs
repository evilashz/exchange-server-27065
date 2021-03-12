using System;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	public sealed class TransactionalRequestJob : RequestJobBase, IDisposable, ISendAsSource
	{
		// Token: 0x06001DBF RID: 7615 RVA: 0x0003D675 File Offset: 0x0003B875
		public TransactionalRequestJob()
		{
			this.moveObject = null;
			this.provider = null;
			this.unknownElements = null;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0003D692 File Offset: 0x0003B892
		internal TransactionalRequestJob(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
			this.moveObject = null;
			this.provider = null;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0003D6A9 File Offset: 0x0003B8A9
		internal TransactionalRequestJob(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
			this.UnknownElements = requestJob.UnknownElements;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0003D6CF File Offset: 0x0003B8CF
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0003D6D7 File Offset: 0x0003B8D7
		internal MoveObjectInfo<RequestJobXML> MoveObject
		{
			get
			{
				return this.moveObject;
			}
			set
			{
				this.moveObject = value;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0003D6E8 File Offset: 0x0003B8E8
		internal RequestJobProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0003D6F1 File Offset: 0x0003B8F1
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0003D6F9 File Offset: 0x0003B8F9
		internal XmlElement[] UnknownElements
		{
			get
			{
				return this.unknownElements;
			}
			set
			{
				this.unknownElements = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0003D702 File Offset: 0x0003B902
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0003D70C File Offset: 0x0003B90C
		internal new string DomainControllerToUpdate
		{
			get
			{
				return base.DomainControllerToUpdate;
			}
			set
			{
				base.DomainControllerToUpdate = value;
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.DomainControllerUpdate, (value != null) ? new DateTime?(DateTime.UtcNow) : null);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0003D745 File Offset: 0x0003B945
		public Guid SourceGuid
		{
			get
			{
				return base.RequestGuid;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0003D74D File Offset: 0x0003B94D
		public SmtpAddress UserEmailAddress
		{
			get
			{
				return base.EmailAddress;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0003D755 File Offset: 0x0003B955
		public bool IsEnabled
		{
			get
			{
				return base.Status != RequestStatus.Failed;
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0003D764 File Offset: 0x0003B964
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0003D773 File Offset: 0x0003B973
		internal void Dispose(bool disposing)
		{
			if (disposing && this.moveObject != null)
			{
				this.moveObject.Dispose();
				this.moveObject = null;
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0003D792 File Offset: 0x0003B992
		internal bool CheckIfUnderlyingMessageHasChanged()
		{
			return this.MoveObject.CheckIfUnderlyingMessageHasChanged();
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0003D7A0 File Offset: 0x0003B9A0
		internal void Refresh()
		{
			RequestJobXML requestJobXML = this.MoveObject.ReadObject(ReadObjectFlags.Refresh);
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				if (propertyDefinition != SimpleProviderObjectSchema.ObjectState && propertyDefinition != SimpleProviderObjectSchema.ExchangeVersion && propertyDefinition != SimpleProviderObjectSchema.Identity)
				{
					this[propertyDefinition] = requestJobXML[propertyDefinition];
				}
			}
			this.UnknownElements = requestJobXML.UnknownElements;
		}

		// Token: 0x04000C56 RID: 3158
		[NonSerialized]
		private MoveObjectInfo<RequestJobXML> moveObject;

		// Token: 0x04000C57 RID: 3159
		[NonSerialized]
		private RequestJobProvider provider;

		// Token: 0x04000C58 RID: 3160
		[NonSerialized]
		private XmlElement[] unknownElements;
	}
}
