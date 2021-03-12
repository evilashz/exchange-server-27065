using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class Inconsistency : IXmlSerializable
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00004435 File Offset: 0x00002635
		protected Inconsistency()
		{
			this.ShouldFix = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004444 File Offset: 0x00002644
		protected Inconsistency(RoleType owner, string description, CalendarInconsistencyFlag flag, CalendarValidationContext context) : this()
		{
			this.Owner = owner;
			this.Description = description;
			this.Flag = flag;
			this.OwnerIsGroupMailbox = context.IsRoleGroupMailbox(this.Owner);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004474 File Offset: 0x00002674
		internal static Inconsistency CreateInstance(RoleType owner, string description, CalendarInconsistencyFlag flag, CalendarValidationContext context)
		{
			return new Inconsistency(owner, description, flag, context);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000447F File Offset: 0x0000267F
		internal static Inconsistency CreateMissingCvsInconsistency(RoleType owner, CalendarVersionStoreNotPopulatedException exc, CalendarValidationContext context)
		{
			return Inconsistency.CreateInstance(owner, string.Format("The Calendar Version Store is not fully populated yet (Wait Time: {0}).", exc.WaitTimeBeforeThrow), CalendarInconsistencyFlag.MissingCvs, context);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000044A0 File Offset: 0x000026A0
		internal virtual RumInfo CreateRumInfo(CalendarValidationContext context, IList<Attendee> attendees)
		{
			switch (this.Flag)
			{
			case CalendarInconsistencyFlag.None:
			case CalendarInconsistencyFlag.StoreObjectValidation:
			case CalendarInconsistencyFlag.StorageException:
			case CalendarInconsistencyFlag.UserNotFound:
			case CalendarInconsistencyFlag.LegacyUser:
			case CalendarInconsistencyFlag.LargeDL:
			case CalendarInconsistencyFlag.RecurrenceAnomaly:
			case CalendarInconsistencyFlag.RecurringException:
			case CalendarInconsistencyFlag.ModifiedOccurrenceMatch:
			case CalendarInconsistencyFlag.DuplicatedItem:
			case CalendarInconsistencyFlag.MissingCvs:
				return NullOpRumInfo.CreateInstance();
			case CalendarInconsistencyFlag.VersionInfo:
			case CalendarInconsistencyFlag.TimeOverlap:
			case CalendarInconsistencyFlag.StartTime:
			case CalendarInconsistencyFlag.EndTime:
			case CalendarInconsistencyFlag.StartTimeZone:
			case CalendarInconsistencyFlag.RecurringTimeZone:
			case CalendarInconsistencyFlag.Location:
			case CalendarInconsistencyFlag.RecurrenceBlob:
			case CalendarInconsistencyFlag.MissingOccurrenceDeletion:
				return UpdateRumInfo.CreateMasterInstance(attendees, this.Flag);
			}
			throw new NotImplementedException(string.Format("Unrecognized inconsistency: {0}", this.Flag));
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004551 File Offset: 0x00002751
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00004559 File Offset: 0x00002759
		internal CalendarInconsistencyFlag Flag { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004562 File Offset: 0x00002762
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000456A File Offset: 0x0000276A
		internal RoleType Owner { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004573 File Offset: 0x00002773
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000457B File Offset: 0x0000277B
		public bool OwnerIsGroupMailbox { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004584 File Offset: 0x00002784
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000458C File Offset: 0x0000278C
		internal string Description { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004595 File Offset: 0x00002795
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000459D File Offset: 0x0000279D
		public bool ShouldFix { get; internal set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000045A6 File Offset: 0x000027A6
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000045AE File Offset: 0x000027AE
		public ClientIntentFlags? Intent { get; internal set; }

		// Token: 0x0600006F RID: 111 RVA: 0x000045B7 File Offset: 0x000027B7
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000045BA File Offset: 0x000027BA
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException("XML deserialization is not supported.");
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000045C8 File Offset: 0x000027C8
		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("Owner", this.Owner.ToString());
			writer.WriteElementString("OwnerIsGroupMailbox", this.OwnerIsGroupMailbox.ToString());
			writer.WriteElementString("Description", this.Description);
		}
	}
}
