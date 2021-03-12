using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000641 RID: 1601
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTagHelper
	{
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x001184D0 File Offset: 0x001166D0
		public static PropertyDefinition[] ArchiveProperties
		{
			get
			{
				return PolicyTagHelper.ArchivePropertyArray;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x001184D7 File Offset: 0x001166D7
		public static PropertyDefinition[] RetentionProperties
		{
			get
			{
				return PolicyTagHelper.RetentionPropertyArray;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x001184DE File Offset: 0x001166DE
		public static PropertyDefinition[] FolderProperties
		{
			get
			{
				return PolicyTagHelper.FolderPropertyArray;
			}
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x001184E8 File Offset: 0x001166E8
		public static void SetPolicyTagForArchiveOnFolder(PolicyTag policyTag, Folder folder)
		{
			folder[StoreObjectSchema.ArchiveTag] = policyTag.PolicyGuid.ToByteArray();
			folder[StoreObjectSchema.ArchivePeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			RetentionAndArchiveFlags retentionAndArchiveFlags;
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault | 16);
			}
			else
			{
				retentionAndArchiveFlags = RetentionAndArchiveFlags.ExplictArchiveTag;
			}
			folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.ExplicitArchiveTag) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ExplicitArchiveTag
				});
			}
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x0011858C File Offset: 0x0011678C
		public static void SetPolicyTagForArchiveOnNewFolder(PolicyTag policyTag, Folder folder)
		{
			folder[StoreObjectSchema.ArchiveTag] = policyTag.PolicyGuid.ToByteArray();
			folder[StoreObjectSchema.ArchivePeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			RetentionAndArchiveFlags retentionAndArchiveFlags;
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault | 16);
			}
			else
			{
				retentionAndArchiveFlags = RetentionAndArchiveFlags.ExplictArchiveTag;
			}
			folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00118608 File Offset: 0x00116808
		public static void ClearPolicyTagForArchiveOnFolder(Folder folder)
		{
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.ArchiveTag) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ArchiveTag
				});
			}
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.ArchivePeriod) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ArchivePeriod
				});
			}
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault & -17);
				folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			}
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00118690 File Offset: 0x00116890
		public static Guid? GetPolicyTagForArchiveFromFolder(Folder folder, out bool isExplicit)
		{
			Guid? result = null;
			byte[] array = (byte[])folder.GetValueOrDefault<object>(StoreObjectSchema.ArchiveTag);
			if (array != null && array.Length == PolicyTagHelper.SizeOfGuid)
			{
				result = new Guid?(new Guid(array));
			}
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				isExplicit = (((int)valueOrDefault & 16) != 0);
			}
			else
			{
				isExplicit = false;
			}
			return result;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00118700 File Offset: 0x00116900
		public static void SetPolicyTagForArchiveOnItem(PolicyTag policyTag, StoreObject item)
		{
			item[StoreObjectSchema.ArchiveTag] = policyTag.PolicyGuid.ToByteArray();
			object valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault & -33);
				item[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			}
			else
			{
				item[StoreObjectSchema.RetentionFlags] = 0;
			}
			CompositeRetentionProperty compositeRetentionProperty = null;
			byte[] valueOrDefault2 = item.GetValueOrDefault<byte[]>(ItemSchema.StartDateEtc);
			if (valueOrDefault2 != null)
			{
				try
				{
					compositeRetentionProperty = CompositeRetentionProperty.Parse(valueOrDefault2, true);
				}
				catch (ArgumentException)
				{
					compositeRetentionProperty = null;
				}
			}
			if (compositeRetentionProperty == null)
			{
				compositeRetentionProperty = new CompositeRetentionProperty();
				compositeRetentionProperty.Integer = (int)policyTag.TimeSpanForRetention.TotalDays;
				valueOrDefault = item.GetValueOrDefault<object>(InternalSchema.ReceivedTime);
				if (valueOrDefault == null)
				{
					valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.CreationTime);
				}
				if (valueOrDefault == null)
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)ExDateTime.Now);
				}
				else
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)((ExDateTime)valueOrDefault));
				}
				item[InternalSchema.StartDateEtc] = compositeRetentionProperty.GetBytes(true);
			}
			if (policyTag.TimeSpanForRetention.TotalDays > 0.0)
			{
				long fileTime = 0L;
				try
				{
					fileTime = compositeRetentionProperty.Date.Value.AddDays(policyTag.TimeSpanForRetention.TotalDays).ToFileTimeUtc();
				}
				catch (ArgumentOutOfRangeException)
				{
					fileTime = 0L;
				}
				item[InternalSchema.ArchivePeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
				DateTime dateTime = DateTime.FromFileTimeUtc(fileTime);
				item[InternalSchema.ArchiveDate] = dateTime;
			}
			else if (item.GetValueOrDefault<object>(InternalSchema.ArchiveDate) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					InternalSchema.ArchiveDate
				});
			}
			if (item.GetValueOrDefault<object>(StoreObjectSchema.ExplicitArchiveTag) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ExplicitArchiveTag
				});
			}
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00118908 File Offset: 0x00116B08
		public static void SetPolicyTagForArchiveOnNewItem(PolicyTag policyTag, StoreObject item)
		{
			item[StoreObjectSchema.ArchiveTag] = policyTag.PolicyGuid.ToByteArray();
			item[StoreObjectSchema.RetentionFlags] = 0;
			CompositeRetentionProperty setStartDateEtc = PolicyTagHelper.GetSetStartDateEtc(policyTag, item);
			if (policyTag.TimeSpanForRetention.TotalDays > 0.0)
			{
				item[InternalSchema.ArchivePeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
				item[InternalSchema.ArchiveDate] = PolicyTagHelper.CalculateExecutionDate(setStartDateEtc, policyTag.TimeSpanForRetention.TotalDays);
			}
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x001189A4 File Offset: 0x00116BA4
		public static void SetKeepInPlaceForArchiveOnItem(StoreObject item)
		{
			PolicyTagHelper.ClearPolicyTagForArchiveOnItem(item);
			object valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			RetentionAndArchiveFlags retentionAndArchiveFlags;
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault | 32);
			}
			else
			{
				retentionAndArchiveFlags = RetentionAndArchiveFlags.KeepInPlace;
			}
			item[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x001189F0 File Offset: 0x00116BF0
		public static void ClearPolicyTagForArchiveOnItem(StoreObject item)
		{
			if (item.GetValueOrDefault<object>(StoreObjectSchema.ArchiveTag) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ArchiveTag
				});
			}
			if (item.GetValueOrDefault<object>(StoreObjectSchema.ArchivePeriod) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ArchivePeriod
				});
			}
			if (item.GetValueOrDefault<object>(InternalSchema.ArchiveDate) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					InternalSchema.ArchiveDate
				});
			}
			object valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault & -33);
				item[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00118A9C File Offset: 0x00116C9C
		public static Guid? GetPolicyTagForArchiveFromItem(StoreObject item, out bool isExplicit, out bool isKeptInPlace, out DateTime? moveToArchive)
		{
			isExplicit = false;
			moveToArchive = null;
			Guid? result = null;
			byte[] array = (byte[])item.GetValueOrDefault<object>(InternalSchema.ArchiveTag);
			if (array != null && array.Length == PolicyTagHelper.SizeOfGuid)
			{
				result = new Guid?(new Guid(array));
			}
			object valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				isKeptInPlace = (((int)valueOrDefault & 32) != 0);
			}
			else
			{
				isKeptInPlace = false;
			}
			isExplicit = (item.GetValueOrDefault<object>(StoreObjectSchema.ArchivePeriod) != null);
			moveToArchive = (DateTime?)item.GetValueAsNullable<ExDateTime>(ItemSchema.ArchiveDate);
			return result;
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00118B40 File Offset: 0x00116D40
		public static void SetPolicyTagForDeleteOnFolder(PolicyTag policyTag, Folder folder)
		{
			folder[StoreObjectSchema.PolicyTag] = policyTag.PolicyGuid.ToByteArray();
			folder[StoreObjectSchema.RetentionPeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			int num = 9;
			RetentionAndArchiveFlags retentionAndArchiveFlags;
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault | num);
			}
			else
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)num;
			}
			folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.ExplicitPolicyTag) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ExplicitPolicyTag
				});
			}
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00118BE4 File Offset: 0x00116DE4
		public static void SetPolicyTagForDeleteOnNewFolder(PolicyTag policyTag, Folder folder)
		{
			folder[StoreObjectSchema.PolicyTag] = policyTag.PolicyGuid.ToByteArray();
			folder[StoreObjectSchema.RetentionPeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			int num = 9;
			RetentionAndArchiveFlags retentionAndArchiveFlags;
			if (valueOrDefault != null && valueOrDefault is int)
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault | num);
			}
			else
			{
				retentionAndArchiveFlags = (RetentionAndArchiveFlags)num;
			}
			folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00118C64 File Offset: 0x00116E64
		public static void ClearPolicyTagForDeleteOnFolder(Folder folder)
		{
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.PolicyTag
				});
			}
			if (folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionPeriod) != null)
			{
				folder.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.RetentionPeriod
				});
			}
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)((int)valueOrDefault & -2);
				retentionAndArchiveFlags &= ~RetentionAndArchiveFlags.PersonalTag;
				folder[StoreObjectSchema.RetentionFlags] = (int)retentionAndArchiveFlags;
			}
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00118CF0 File Offset: 0x00116EF0
		public static Guid? GetPolicyTagForDeleteFromFolder(Folder folder, out bool isExplicit)
		{
			Guid? result = null;
			byte[] array = (byte[])folder.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag);
			if (array != null && array.Length == PolicyTagHelper.SizeOfGuid)
			{
				result = new Guid?(new Guid(array));
			}
			object valueOrDefault = folder.GetValueOrDefault<object>(StoreObjectSchema.RetentionFlags);
			if (valueOrDefault != null && valueOrDefault is int)
			{
				isExplicit = (((int)valueOrDefault & 1) != 0);
			}
			else
			{
				isExplicit = false;
			}
			return result;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x00118D60 File Offset: 0x00116F60
		public static void SetPolicyTagForDeleteOnItem(PolicyTag policyTag, StoreObject item)
		{
			item[StoreObjectSchema.PolicyTag] = policyTag.PolicyGuid.ToByteArray();
			CompositeRetentionProperty compositeRetentionProperty = null;
			byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(ItemSchema.StartDateEtc);
			if (valueOrDefault != null)
			{
				try
				{
					compositeRetentionProperty = CompositeRetentionProperty.Parse(valueOrDefault, true);
				}
				catch (ArgumentException)
				{
					compositeRetentionProperty = null;
				}
			}
			if (compositeRetentionProperty == null)
			{
				compositeRetentionProperty = new CompositeRetentionProperty();
				compositeRetentionProperty.Integer = (int)policyTag.TimeSpanForRetention.TotalDays;
				object valueOrDefault2 = item.GetValueOrDefault<object>(InternalSchema.ReceivedTime);
				if (valueOrDefault2 == null)
				{
					valueOrDefault2 = item.GetValueOrDefault<object>(StoreObjectSchema.CreationTime);
				}
				if (valueOrDefault2 == null)
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)ExDateTime.Now);
				}
				else
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)((ExDateTime)valueOrDefault2));
				}
				item[InternalSchema.StartDateEtc] = compositeRetentionProperty.GetBytes(true);
			}
			long fileTime = 0L;
			try
			{
				fileTime = compositeRetentionProperty.Date.Value.AddDays(policyTag.TimeSpanForRetention.TotalDays).ToFileTimeUtc();
			}
			catch (ArgumentOutOfRangeException)
			{
				fileTime = 0L;
			}
			item[InternalSchema.RetentionPeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
			DateTime dateTime = DateTime.FromFileTimeUtc(fileTime);
			item[InternalSchema.RetentionDate] = dateTime;
			if (item.GetValueOrDefault<object>(StoreObjectSchema.ExplicitPolicyTag) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.ExplicitPolicyTag
				});
			}
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x00118EDC File Offset: 0x001170DC
		public static void SetPolicyTagForDeleteOnNewItem(PolicyTag policyTag, StoreObject item)
		{
			item[StoreObjectSchema.PolicyTag] = policyTag.PolicyGuid.ToByteArray();
			item[StoreObjectSchema.RetentionFlags] = 0;
			CompositeRetentionProperty setStartDateEtc = PolicyTagHelper.GetSetStartDateEtc(policyTag, item);
			if (policyTag.TimeSpanForRetention.TotalDays > 0.0)
			{
				item[InternalSchema.RetentionPeriod] = (int)policyTag.TimeSpanForRetention.TotalDays;
				item[InternalSchema.RetentionDate] = PolicyTagHelper.CalculateExecutionDate(setStartDateEtc, policyTag.TimeSpanForRetention.TotalDays);
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x00118F78 File Offset: 0x00117178
		public static void ClearPolicyTagForDeleteOnItem(StoreObject item)
		{
			if (item.GetValueOrDefault<object>(StoreObjectSchema.PolicyTag) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.PolicyTag
				});
			}
			if (item.GetValueOrDefault<object>(StoreObjectSchema.RetentionPeriod) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					StoreObjectSchema.RetentionPeriod
				});
			}
			if (item.GetValueOrDefault<object>(InternalSchema.RetentionDate) != null)
			{
				item.DeleteProperties(new PropertyDefinition[]
				{
					InternalSchema.RetentionDate
				});
			}
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x00118FF0 File Offset: 0x001171F0
		public static Guid? GetPolicyTagForDeleteFromItem(StoreObject item, out bool isExplicit, out DateTime? deleteTime)
		{
			isExplicit = false;
			deleteTime = null;
			Guid? result = null;
			byte[] array = (byte[])item.GetValueOrDefault<object>(InternalSchema.PolicyTag);
			if (array != null && array.Length == PolicyTagHelper.SizeOfGuid)
			{
				result = new Guid?(new Guid(array));
			}
			isExplicit = (item.GetValueOrDefault<object>(StoreObjectSchema.RetentionPeriod) != null);
			deleteTime = (DateTime?)item.GetValueAsNullable<ExDateTime>(ItemSchema.RetentionDate);
			return result;
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x00119064 File Offset: 0x00117264
		public static void SetRetentionProperties(StoreObject item, ExDateTime dateToExpireOn, int retentionPeriodInDays)
		{
			if (!(item is Item))
			{
				throw new ArgumentException("item must be of type Item. It cannot be null, a folder or anything else.");
			}
			if (retentionPeriodInDays <= 0)
			{
				throw new ArgumentException("retentionPeriodInDays must be greater than 0. The minimum value allowed is 1 day.");
			}
			item[StoreObjectSchema.PolicyTag] = PolicyTagHelper.SystemCleanupTagGuid.ToByteArray();
			item[ItemSchema.RetentionDate] = (DateTime)dateToExpireOn.ToUtc();
			item[StoreObjectSchema.RetentionPeriod] = retentionPeriodInDays;
			item[StoreObjectSchema.RetentionFlags] = 64;
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x001190EC File Offset: 0x001172EC
		public static void ApplyPolicyToFolder(MailboxSession session, CoreFolder coreFolder)
		{
			if (coreFolder.Origin == Origin.Existing)
			{
				PropertyDefinition[] folderProperties = PolicyTagHelper.FolderProperties;
				for (int i = 0; i < folderProperties.Length; i++)
				{
					if (coreFolder.PropertyBag.IsPropertyDirty(folderProperties[i]))
					{
						int num = 0;
						object obj = coreFolder.PropertyBag.TryGetProperty(InternalSchema.RetentionFlags);
						if (obj is int)
						{
							num = (int)obj;
						}
						num = FlagsMan.SetNeedRescan(num);
						coreFolder.PropertyBag.SetProperty(InternalSchema.RetentionFlags, num);
						return;
					}
				}
				return;
			}
			int num2 = 0;
			object obj2 = coreFolder.PropertyBag.TryGetProperty(InternalSchema.RetentionFlags);
			if (obj2 is int)
			{
				num2 = (int)obj2;
			}
			num2 = FlagsMan.SetNeedRescan(num2);
			coreFolder.PropertyBag.SetProperty(InternalSchema.RetentionFlags, num2);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x001191B0 File Offset: 0x001173B0
		private static CompositeRetentionProperty GetSetStartDateEtc(PolicyTag policyTag, StoreObject item)
		{
			CompositeRetentionProperty compositeRetentionProperty = null;
			byte[] array = null;
			try
			{
				array = item.GetValueOrDefault<byte[]>(InternalSchema.StartDateEtc);
			}
			catch (NotInBagPropertyErrorException)
			{
				array = null;
			}
			if (array != null)
			{
				try
				{
					compositeRetentionProperty = CompositeRetentionProperty.Parse(array, true);
				}
				catch (ArgumentException)
				{
					compositeRetentionProperty = null;
				}
			}
			if (compositeRetentionProperty == null)
			{
				compositeRetentionProperty = new CompositeRetentionProperty();
				compositeRetentionProperty.Integer = (int)policyTag.TimeSpanForRetention.TotalDays;
				object valueOrDefault = item.GetValueOrDefault<object>(InternalSchema.ReceivedTime);
				if (valueOrDefault == null)
				{
					valueOrDefault = item.GetValueOrDefault<object>(StoreObjectSchema.CreationTime);
				}
				if (valueOrDefault == null)
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)ExDateTime.Now);
				}
				else
				{
					compositeRetentionProperty.Date = new DateTime?((DateTime)((ExDateTime)valueOrDefault));
				}
				item[InternalSchema.StartDateEtc] = compositeRetentionProperty.GetBytes(true);
			}
			return compositeRetentionProperty;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00119280 File Offset: 0x00117480
		private static DateTime CalculateExecutionDate(CompositeRetentionProperty startDateEtc, double policyDays)
		{
			long fileTime = 0L;
			if (startDateEtc != null && startDateEtc.Date != null)
			{
				try
				{
					fileTime = startDateEtc.Date.Value.AddDays(policyDays).ToFileTimeUtc();
				}
				catch (ArgumentOutOfRangeException)
				{
					fileTime = 0L;
				}
			}
			return DateTime.FromFileTimeUtc(fileTime);
		}

		// Token: 0x04002472 RID: 9330
		public static readonly Guid SystemCleanupTagGuid = new Guid("CCE0D6E6-B69B-410a-907D-06DC2071AB58");

		// Token: 0x04002473 RID: 9331
		private static readonly int SizeOfGuid = Marshal.SizeOf(Guid.NewGuid());

		// Token: 0x04002474 RID: 9332
		private static readonly PropertyDefinition[] FolderPropertyArray = new PropertyDefinition[]
		{
			InternalSchema.PolicyTag,
			InternalSchema.ArchiveTag,
			InternalSchema.RetentionPeriod,
			InternalSchema.ArchivePeriod,
			InternalSchema.RetentionFlags
		};

		// Token: 0x04002475 RID: 9333
		private static readonly PropertyDefinition[] ArchivePropertyArray = new PropertyDefinition[]
		{
			InternalSchema.ArchiveTag,
			InternalSchema.ArchiveDate,
			InternalSchema.ArchivePeriod,
			InternalSchema.RetentionFlags,
			InternalSchema.StartDateEtc,
			InternalSchema.ExplicitArchiveTag
		};

		// Token: 0x04002476 RID: 9334
		private static readonly PropertyDefinition[] RetentionPropertyArray = new PropertyDefinition[]
		{
			InternalSchema.PolicyTag,
			InternalSchema.RetentionDate,
			InternalSchema.RetentionPeriod,
			InternalSchema.RetentionFlags,
			InternalSchema.StartDateEtc,
			InternalSchema.ExplicitPolicyTag
		};
	}
}
