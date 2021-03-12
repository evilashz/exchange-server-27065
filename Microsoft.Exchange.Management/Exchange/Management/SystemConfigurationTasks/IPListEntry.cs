using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.IPFilter;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A66 RID: 2662
	[Serializable]
	public abstract class IPListEntry : IConfigurable
	{
		// Token: 0x06005F28 RID: 24360 RVA: 0x0018F264 File Offset: 0x0018D464
		internal static T NewIPListEntry<T>(IPFilterRange range) where T : IConfigurable, new()
		{
			IPListEntry iplistEntry = (IPListEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			IPRange.Format format = (IPRange.Format)(range.Flags & 15);
			IPListEntryType iplistEntryType = (IPListEntryType)(range.Flags >> 4 & 15);
			IPListEntry.AuthorType authorType = (IPListEntry.AuthorType)(range.Flags >> 8 & 15);
			iplistEntry.identity = new IPListEntryIdentity(range.Identity);
			ulong high;
			ulong low;
			range.GetLowerBound(out high, out low);
			IPvxAddress startAddress = new IPvxAddress(high, low);
			range.GetUpperBound(out high, out low);
			IPvxAddress endAddress = new IPvxAddress(high, low);
			iplistEntry.range = new IPRange(startAddress, endAddress, format);
			iplistEntry.expirationTimeUtc = range.ExpiresOn.ToUniversalTime();
			iplistEntry.isMachineGenerated = (authorType == IPListEntry.AuthorType.Automatic);
			iplistEntry.listType = iplistEntryType;
			iplistEntry.comment = range.Comment;
			return (T)((object)iplistEntry);
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0018F344 File Offset: 0x0018D544
		protected IPListEntry()
		{
			this.identity = null;
			this.range = null;
			this.expirationTimeUtc = DateTime.MaxValue;
			this.isMachineGenerated = false;
			this.listType = this.ListType;
			this.isValid = true;
			this.comment = string.Empty;
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06005F2A RID: 24362
		public abstract IPListEntryType ListType { get; }

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x0018F395 File Offset: 0x0018D595
		// (set) Token: 0x06005F2C RID: 24364 RVA: 0x0018F39D File Offset: 0x0018D59D
		public IPRange IPRange
		{
			get
			{
				return this.range;
			}
			set
			{
				this.range = value;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x0018F3A6 File Offset: 0x0018D5A6
		// (set) Token: 0x06005F2E RID: 24366 RVA: 0x0018F3B3 File Offset: 0x0018D5B3
		public DateTime ExpirationTime
		{
			get
			{
				return this.expirationTimeUtc.ToLocalTime();
			}
			set
			{
				this.expirationTimeUtc = value.ToUniversalTime();
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x0018F3C2 File Offset: 0x0018D5C2
		// (set) Token: 0x06005F30 RID: 24368 RVA: 0x0018F3CA File Offset: 0x0018D5CA
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x0018F3D3 File Offset: 0x0018D5D3
		public bool IsMachineGenerated
		{
			get
			{
				return this.isMachineGenerated;
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x0018F3DB File Offset: 0x0018D5DB
		public bool HasExpired
		{
			get
			{
				return this.expirationTimeUtc < DateTime.UtcNow;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06005F33 RID: 24371 RVA: 0x0018F3ED File Offset: 0x0018D5ED
		// (set) Token: 0x06005F34 RID: 24372 RVA: 0x0018F3F5 File Offset: 0x0018D5F5
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
			internal set
			{
				this.identity = (IPListEntryIdentity)value;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06005F35 RID: 24373 RVA: 0x0018F403 File Offset: 0x0018D603
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06005F36 RID: 24374 RVA: 0x0018F40B File Offset: 0x0018D60B
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x0018F410 File Offset: 0x0018D610
		internal IPFilterRange ToIPFilterRange()
		{
			IPFilterRange ipfilterRange = new IPFilterRange();
			ipfilterRange.ExpiresOn = this.expirationTimeUtc;
			IPvxAddress pvxAddress = this.IPRange.LowerBound;
			ipfilterRange.SetLowerBound((ulong)(pvxAddress >> 64), (ulong)pvxAddress);
			pvxAddress = this.IPRange.UpperBound;
			ipfilterRange.SetUpperBound((ulong)(pvxAddress >> 64), (ulong)pvxAddress);
			ipfilterRange.Flags = (int)this.IPRange.RangeFormat;
			ipfilterRange.Comment = this.comment;
			return ipfilterRange;
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x0018F498 File Offset: 0x0018D698
		public ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (this.listType != this.ListType)
			{
				list.Add(new PropertyValidationError(Strings.IPListEntryTypeMismatch, null, this.ListType));
			}
			this.isValid = (list.Count == 0);
			return list.ToArray();
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x0018F4EC File Offset: 0x0018D6EC
		public void CopyChangesFrom(IConfigurable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IPListEntry iplistEntry = source as IPListEntry;
			if (iplistEntry == null)
			{
				throw new ArgumentException(DirectoryStrings.ExceptionCopyChangesForIncompatibleTypes(source.GetType(), base.GetType()), "source");
			}
			if (iplistEntry.identity != null)
			{
				this.identity = new IPListEntryIdentity(iplistEntry.identity.Index);
			}
			if (iplistEntry.range != null)
			{
				this.range = new IPRange(new IPvxAddress(iplistEntry.range.LowerBound), new IPvxAddress(iplistEntry.range.UpperBound), iplistEntry.range.RangeFormat);
			}
			this.expirationTimeUtc = iplistEntry.expirationTimeUtc;
			this.isMachineGenerated = iplistEntry.IsMachineGenerated;
			this.listType = iplistEntry.ListType;
			this.isValid = iplistEntry.IsValid;
			this.comment = iplistEntry.Comment;
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x0018F5D0 File Offset: 0x0018D7D0
		public void ResetChangeTracking()
		{
			throw new NotSupportedException("IPListEntry.CopyChangesFrom");
		}

		// Token: 0x04003513 RID: 13587
		private IPListEntryIdentity identity;

		// Token: 0x04003514 RID: 13588
		private IPRange range;

		// Token: 0x04003515 RID: 13589
		private DateTime expirationTimeUtc;

		// Token: 0x04003516 RID: 13590
		private bool isMachineGenerated;

		// Token: 0x04003517 RID: 13591
		private IPListEntryType listType;

		// Token: 0x04003518 RID: 13592
		private bool isValid;

		// Token: 0x04003519 RID: 13593
		private string comment;

		// Token: 0x02000A67 RID: 2663
		private enum AuthorType
		{
			// Token: 0x0400351B RID: 13595
			Undefined,
			// Token: 0x0400351C RID: 13596
			Manual,
			// Token: 0x0400351D RID: 13597
			Automatic
		}

		// Token: 0x02000A68 RID: 2664
		internal struct FieldNames
		{
			// Token: 0x0400351E RID: 13598
			public const string IPAddress = "IPAddress";
		}
	}
}
