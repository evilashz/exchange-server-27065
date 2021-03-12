using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class ActivityLogEntryPresentationObject : ConfigurableObject
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000040C5 File Offset: 0x000022C5
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ActivityLogEntryPresentationObject.Schema;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000040CC File Offset: 0x000022CC
		internal ActivityLogEntryPresentationObject(Activity activity) : this()
		{
			this.ClientId = activity.ClientId.ToString();
			this.ActivityId = Enum.GetName(typeof(ActivityId), activity.Id);
			this.TimeStamp = activity.TimeStamp;
			object itemId = activity.ItemId;
			this.CustomProperties = activity.CustomPropertiesString;
			if (itemId != null)
			{
				this.EntryId = ((StoreObjectId)itemId).ToBase64String();
			}
			base.ResetChangeTracking();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004149 File Offset: 0x00002349
		public ActivityLogEntryPresentationObject() : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004161 File Offset: 0x00002361
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004173 File Offset: 0x00002373
		[Parameter(Mandatory = true)]
		public string ClientId
		{
			get
			{
				return (string)this[ActivityLogEntryPresentationObjectSchema.ClientId];
			}
			set
			{
				this[ActivityLogEntryPresentationObjectSchema.ClientId] = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004181 File Offset: 0x00002381
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004193 File Offset: 0x00002393
		[Parameter(Mandatory = true)]
		public string ActivityId
		{
			get
			{
				return (string)this[ActivityLogEntryPresentationObjectSchema.ActivityId];
			}
			set
			{
				this[ActivityLogEntryPresentationObjectSchema.ActivityId] = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000041B3 File Offset: 0x000023B3
		[Parameter(Mandatory = true)]
		public ExDateTime TimeStamp
		{
			get
			{
				return (ExDateTime)this[ActivityLogEntryPresentationObjectSchema.TimeStamp];
			}
			set
			{
				this[ActivityLogEntryPresentationObjectSchema.TimeStamp] = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000041C6 File Offset: 0x000023C6
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000041D8 File Offset: 0x000023D8
		[Parameter(Mandatory = true)]
		public string EntryId
		{
			get
			{
				return (string)this[ActivityLogEntryPresentationObjectSchema.EntryId];
			}
			set
			{
				this[ActivityLogEntryPresentationObjectSchema.EntryId] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000041E6 File Offset: 0x000023E6
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000041F8 File Offset: 0x000023F8
		[Parameter(Mandatory = true)]
		public string CustomProperties
		{
			get
			{
				return (string)this[ActivityLogEntryPresentationObjectSchema.CustomProperties];
			}
			set
			{
				this[ActivityLogEntryPresentationObjectSchema.CustomProperties] = value;
			}
		}

		// Token: 0x0400004B RID: 75
		private static readonly ActivityLogEntryPresentationObjectSchema Schema = ObjectSchema.GetInstance<ActivityLogEntryPresentationObjectSchema>();
	}
}
