using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200024E RID: 590
	public class MailboxLocationCollection : IMailboxLocationCollection
	{
		// Token: 0x06001CF1 RID: 7409 RVA: 0x00077E84 File Offset: 0x00076084
		internal MailboxLocationCollection(IPropertyBag propertyBag)
		{
			this.Initialize();
			if (propertyBag[IADMailStorageSchema.MailboxLocationsRaw] == null)
			{
				propertyBag[IADMailStorageSchema.MailboxLocationsRaw] = new MultiValuedProperty<ADObjectIdWithString>(false, IADMailStorageSchema.MailboxLocationsRaw, new ADObjectIdWithString[0]);
			}
			if (propertyBag[IADMailStorageSchema.MailboxGuidsRaw] == null)
			{
				propertyBag[IADMailStorageSchema.MailboxGuidsRaw] = new MultiValuedProperty<Guid>(false, IADMailStorageSchema.MailboxGuidsRaw, new Guid[0]);
			}
			this.locations = (propertyBag[IADMailStorageSchema.MailboxLocationsRaw] as MultiValuedProperty<ADObjectIdWithString>);
			this.guids = (propertyBag[IADMailStorageSchema.MailboxGuidsRaw] as MultiValuedProperty<Guid>);
			this.propertyBag = propertyBag;
			if (this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.Primary) != null)
			{
				this.mailboxTypeCount[0]++;
			}
			if (this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.MainArchive) != null)
			{
				this.mailboxTypeCount[1]++;
			}
			foreach (ADObjectIdWithString adobjectIdWithString in this.locations)
			{
				IMailboxLocationInfo info = new MailboxLocationInfo(adobjectIdWithString.StringValue);
				this.ValidateAndAddMailboxInfo(info, false);
			}
			this.isInitialGuidsValid = this.ValidRawGuids();
			if (!this.isInitialGuidsValid)
			{
				this.guids.Clear();
				foreach (Guid item in this.mailboxInfo.Keys)
				{
					this.guids.Add(item);
				}
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00078028 File Offset: 0x00076228
		public MailboxLocationCollection()
		{
			this.Initialize();
			this.propertyBag = null;
			this.newArchiveAndExchangeInfo = new List<IMailboxLocationInfo>();
			this.locations = new MultiValuedProperty<ADObjectIdWithString>();
			this.guids = new MultiValuedProperty<Guid>();
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00078060 File Offset: 0x00076260
		public IMailboxLocationInfo GetMailboxLocation(MailboxLocationType mailboxLocationType)
		{
			if (MailboxLocationCollection.NonSingletonMailboxLocationType.Contains(mailboxLocationType))
			{
				throw new MailboxLocationException(DirectoryStrings.ErrorNonUniqueMailboxGetMailboxLocation(mailboxLocationType.ToString()));
			}
			if (this.mailboxTypeCount[(int)mailboxLocationType] == 0)
			{
				return null;
			}
			IList<IMailboxLocationInfo> mailboxLocations = this.GetMailboxLocations(mailboxLocationType);
			if (mailboxLocations.Count != 0)
			{
				return mailboxLocations[0];
			}
			return null;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000780B8 File Offset: 0x000762B8
		public IList<IMailboxLocationInfo> GetMailboxLocations(MailboxLocationType mailboxLocationType)
		{
			IList<IMailboxLocationInfo> list = new List<IMailboxLocationInfo>();
			if (mailboxLocationType == MailboxLocationType.Primary || mailboxLocationType == MailboxLocationType.MainArchive)
			{
				IMailboxLocationInfo primaryOrArchiveMailboxInfo = this.GetPrimaryOrArchiveMailboxInfo(mailboxLocationType);
				if (primaryOrArchiveMailboxInfo != null)
				{
					list.Add(primaryOrArchiveMailboxInfo);
				}
			}
			else
			{
				foreach (IMailboxLocationInfo mailboxLocationInfo in this.mailboxInfo.Values)
				{
					if (mailboxLocationInfo.MailboxLocationType == mailboxLocationType)
					{
						list.Add(mailboxLocationInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00078138 File Offset: 0x00076338
		public IList<IMailboxLocationInfo> GetMailboxLocations()
		{
			IList<IMailboxLocationInfo> list = this.mailboxInfo.Values.ToList<IMailboxLocationInfo>();
			foreach (IMailboxLocationInfo mailboxLocationInfo in new IMailboxLocationInfo[]
			{
				this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.Primary),
				this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.MainArchive)
			})
			{
				if (mailboxLocationInfo != null)
				{
					list.Add(mailboxLocationInfo);
				}
			}
			return list;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00078195 File Offset: 0x00076395
		public IMailboxLocationInfo GetMailboxLocation(Guid mailboxGuid)
		{
			if (mailboxGuid.Equals(Guid.Empty))
			{
				throw new MailboxLocationException(DirectoryStrings.ErrorEmptyString("mailboxGuid"));
			}
			return this.GetMailboxInfo(mailboxGuid);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000781BC File Offset: 0x000763BC
		public void AddMailboxLocation(IMailboxLocationInfo mailboxLocation)
		{
			this.ValidateAndAddMailboxInfo(mailboxLocation, true);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000781C8 File Offset: 0x000763C8
		public void AddMailboxLocation(Guid mailboxGuid, ADObjectId databaseLocation, MailboxLocationType mailboxLocationType)
		{
			IMailboxLocationInfo mailboxLocation = new MailboxLocationInfo(mailboxGuid, databaseLocation, mailboxLocationType);
			this.AddMailboxLocation(mailboxLocation);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000781E8 File Offset: 0x000763E8
		public void RemoveMailboxLocation(Guid mailboxGuid)
		{
			IMailboxLocationInfo mailboxLocationInfo = this.GetMailboxInfo(mailboxGuid);
			if (mailboxLocationInfo != null)
			{
				if (mailboxLocationInfo.MailboxLocationType == MailboxLocationType.Primary || mailboxLocationInfo.MailboxLocationType == MailboxLocationType.MainArchive)
				{
					this.RemovePrimaryOrArchiveMailboxInfo(mailboxLocationInfo.MailboxLocationType);
				}
				else
				{
					this.locations.Remove(new ADObjectIdWithString(mailboxLocationInfo.ToString(), new ADObjectId()));
					this.mailboxInfo.Remove(mailboxGuid);
					this.guids.Remove(mailboxGuid);
				}
				this.mailboxTypeCount[(int)mailboxLocationInfo.MailboxLocationType]--;
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00078274 File Offset: 0x00076474
		public void Validate(List<ValidationError> errors)
		{
			if (this.locations.Count != this.guids.Count)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorInternalLocationsCountMissMatch, IADMailStorageSchema.MailboxLocations, null));
				return;
			}
			if (this.propertyBag != null)
			{
				if (!this.ValidRawGuids())
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorIncorrectlyModifiedMailboxCollection(IADMailStorageSchema.MailboxGuidsRaw.Name), IADMailStorageSchema.MailboxGuidsRaw, null));
				}
				if (!this.ValidMailboxLocationsRaw())
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorIncorrectlyModifiedMailboxCollection(IADMailStorageSchema.MailboxLocationsRaw.Name), IADMailStorageSchema.MailboxLocationsRaw, null));
				}
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00078308 File Offset: 0x00076508
		public override bool Equals(object obj)
		{
			IMailboxLocationCollection mailboxLocationCollection = obj as IMailboxLocationCollection;
			if (mailboxLocationCollection == null)
			{
				return false;
			}
			IList<IMailboxLocationInfo> mailboxLocations = mailboxLocationCollection.GetMailboxLocations();
			IDictionary<Guid, IMailboxLocationInfo> locationsAsDictionary = this.GetLocationsAsDictionary();
			if (locationsAsDictionary.Count != mailboxLocations.Count)
			{
				return false;
			}
			foreach (IMailboxLocationInfo mailboxLocationInfo in mailboxLocations)
			{
				if (!locationsAsDictionary.ContainsKey(mailboxLocationInfo.MailboxGuid) || !locationsAsDictionary[mailboxLocationInfo.MailboxGuid].Equals(mailboxLocationInfo))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000783A4 File Offset: 0x000765A4
		public override int GetHashCode()
		{
			return this.GetLocationsAsDictionary().GetHashCode();
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000783B4 File Offset: 0x000765B4
		private IMailboxLocationInfo GetMailboxInfo(Guid guid)
		{
			if (this.mailboxInfo.ContainsKey(guid))
			{
				return this.mailboxInfo[guid];
			}
			foreach (IMailboxLocationInfo mailboxLocationInfo in new IMailboxLocationInfo[]
			{
				this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.Primary),
				this.GetPrimaryOrArchiveMailboxInfo(MailboxLocationType.MainArchive)
			})
			{
				if (mailboxLocationInfo != null && mailboxLocationInfo.MailboxGuid.Equals(guid))
				{
					return mailboxLocationInfo;
				}
			}
			return null;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0007842C File Offset: 0x0007662C
		private bool ValidMailboxLocationsRaw()
		{
			if (this.propertyBag != null)
			{
				MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = this.propertyBag[IADMailStorageSchema.MailboxLocationsRaw] as MultiValuedProperty<ADObjectIdWithString>;
				if (multiValuedProperty.Count != this.mailboxInfo.Count)
				{
					return false;
				}
				foreach (ADObjectIdWithString adobjectIdWithString in multiValuedProperty)
				{
					IMailboxLocationInfo mailboxLocationInfo = new MailboxLocationInfo(adobjectIdWithString.StringValue);
					if (!this.mailboxInfo.ContainsKey(mailboxLocationInfo.MailboxGuid))
					{
						return false;
					}
					if (!this.mailboxInfo[mailboxLocationInfo.MailboxGuid].Equals(mailboxLocationInfo))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000784F0 File Offset: 0x000766F0
		private bool ValidRawGuids()
		{
			if (this.propertyBag != null)
			{
				MultiValuedProperty<Guid> multiValuedProperty = this.propertyBag[IADMailStorageSchema.MailboxGuidsRaw] as MultiValuedProperty<Guid>;
				if (multiValuedProperty.Count != this.mailboxInfo.Count)
				{
					return false;
				}
				foreach (Guid key in multiValuedProperty)
				{
					if (!this.mailboxInfo.ContainsKey(key))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00078580 File Offset: 0x00076780
		private void ValidateAndAddMailboxInfo(IMailboxLocationInfo info, bool addToMvp)
		{
			if (MailboxLocationCollection.NonSupportedMailboxLocationType.Contains(info.MailboxLocationType))
			{
				throw new MailboxLocationException(DirectoryStrings.ErrorMailboxCollectionNotSupportType(info.MailboxLocationType.ToString()));
			}
			if (!MailboxLocationCollection.NonSingletonMailboxLocationType.Contains(info.MailboxLocationType) && this.mailboxTypeCount[(int)info.MailboxLocationType] >= 1)
			{
				throw new MailboxLocationException(DirectoryStrings.ErrorSingletonMailboxLocationType(info.MailboxLocationType.ToString()));
			}
			if (this.mailboxInfo.ContainsKey(info.MailboxGuid))
			{
				throw new MailboxLocationException(DirectoryStrings.ErrorMailboxExistsInCollection(info.MailboxGuid.ToString()));
			}
			if (info.MailboxLocationType == MailboxLocationType.Primary || info.MailboxLocationType == MailboxLocationType.MainArchive)
			{
				this.AddPrimaryOrArchiveMailboxInfo(info);
			}
			else
			{
				if (addToMvp)
				{
					this.locations.Add(new ADObjectIdWithString(info.ToString(), new ADObjectId()));
					this.guids.Add(info.MailboxGuid);
				}
				this.mailboxInfo[info.MailboxGuid] = info;
			}
			this.mailboxTypeCount[(int)info.MailboxLocationType]++;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000786A2 File Offset: 0x000768A2
		private void Initialize()
		{
			this.mailboxInfo = new Dictionary<Guid, IMailboxLocationInfo>();
			this.mailboxTypeCount = new int[MailboxLocationCollection.MailboxLocationTypeLength];
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000786C0 File Offset: 0x000768C0
		private void AddPrimaryOrArchiveMailboxInfo(IMailboxLocationInfo locationInfo)
		{
			this.ValidateIsPrimaryOrArchive(locationInfo.MailboxLocationType);
			if (this.propertyBag == null)
			{
				this.newArchiveAndExchangeInfo.Add(locationInfo);
				return;
			}
			if (locationInfo.MailboxLocationType == MailboxLocationType.Primary)
			{
				this.propertyBag[IADMailStorageSchema.ExchangeGuid] = locationInfo.MailboxGuid;
				this.propertyBag[IADMailStorageSchema.Database] = locationInfo.DatabaseLocation;
				return;
			}
			this.propertyBag[IADMailStorageSchema.ArchiveGuid] = locationInfo.MailboxGuid;
			ADRecipient.ArchiveDatabaseSetter(locationInfo.DatabaseLocation, this.propertyBag);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00078754 File Offset: 0x00076954
		private void RemovePrimaryOrArchiveMailboxInfo(MailboxLocationType mailboxLocationType)
		{
			this.ValidateIsPrimaryOrArchive(mailboxLocationType);
			if (this.propertyBag == null)
			{
				for (int i = 0; i < this.newArchiveAndExchangeInfo.Count; i++)
				{
					if (this.newArchiveAndExchangeInfo[i].MailboxLocationType == mailboxLocationType)
					{
						this.newArchiveAndExchangeInfo.RemoveAt(i);
						return;
					}
				}
				return;
			}
			if (mailboxLocationType == MailboxLocationType.Primary)
			{
				this.propertyBag[IADMailStorageSchema.ExchangeGuid] = null;
				this.propertyBag[IADMailStorageSchema.Database] = null;
				return;
			}
			this.propertyBag[IADMailStorageSchema.ArchiveGuid] = null;
			ADRecipient.ArchiveDatabaseSetter(null, this.propertyBag);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000787EC File Offset: 0x000769EC
		private IMailboxLocationInfo GetPrimaryOrArchiveMailboxInfo(MailboxLocationType locationType)
		{
			this.ValidateIsPrimaryOrArchive(locationType);
			if (this.propertyBag != null)
			{
				if (locationType == MailboxLocationType.Primary)
				{
					if (this.propertyBag[IADMailStorageSchema.ExchangeGuid] != null && !((Guid)this.propertyBag[IADMailStorageSchema.ExchangeGuid]).Equals(Guid.Empty))
					{
						return new MailboxLocationInfo((Guid)this.propertyBag[IADMailStorageSchema.ExchangeGuid], (ADObjectId)this.propertyBag[IADMailStorageSchema.Database], MailboxLocationType.Primary);
					}
				}
				else if (this.propertyBag[IADMailStorageSchema.ArchiveGuid] != null && !((Guid)this.propertyBag[IADMailStorageSchema.ArchiveGuid]).Equals(Guid.Empty))
				{
					return new MailboxLocationInfo((Guid)this.propertyBag[IADMailStorageSchema.ArchiveGuid], (ADObjectId)ADRecipient.ArchiveDatabaseGetter(this.propertyBag), MailboxLocationType.MainArchive);
				}
			}
			else
			{
				foreach (IMailboxLocationInfo mailboxLocationInfo in this.newArchiveAndExchangeInfo)
				{
					if (mailboxLocationInfo.MailboxLocationType == locationType)
					{
						return mailboxLocationInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00078930 File Offset: 0x00076B30
		private void ValidateIsPrimaryOrArchive(MailboxLocationType locationType)
		{
			if (locationType != MailboxLocationType.Primary && locationType != MailboxLocationType.MainArchive)
			{
				throw new ADOperationException(DirectoryStrings.ExArgumentException("locationType", MailboxLocationType.Primary + ", " + MailboxLocationType.MainArchive));
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00078960 File Offset: 0x00076B60
		private IDictionary<Guid, IMailboxLocationInfo> GetLocationsAsDictionary()
		{
			IDictionary<Guid, IMailboxLocationInfo> dictionary = new Dictionary<Guid, IMailboxLocationInfo>();
			foreach (IMailboxLocationInfo mailboxLocationInfo in this.GetMailboxLocations())
			{
				dictionary[mailboxLocationInfo.MailboxGuid] = mailboxLocationInfo;
			}
			return dictionary;
		}

		// Token: 0x04000DB7 RID: 3511
		public static ISet<MailboxLocationType> NonSingletonMailboxLocationType = new HashSet<MailboxLocationType>(new MailboxLocationType[]
		{
			MailboxLocationType.AuxArchive,
			MailboxLocationType.AuxPrimary
		});

		// Token: 0x04000DB8 RID: 3512
		public static ISet<MailboxLocationType> NonSupportedMailboxLocationType = new HashSet<MailboxLocationType>(new MailboxLocationType[]
		{
			MailboxLocationType.Aggregated
		});

		// Token: 0x04000DB9 RID: 3513
		private static int MailboxLocationTypeLength = Enum.GetValues(typeof(MailboxLocationType)).Length;

		// Token: 0x04000DBA RID: 3514
		private MultiValuedProperty<ADObjectIdWithString> locations;

		// Token: 0x04000DBB RID: 3515
		private MultiValuedProperty<Guid> guids;

		// Token: 0x04000DBC RID: 3516
		private IDictionary<Guid, IMailboxLocationInfo> mailboxInfo;

		// Token: 0x04000DBD RID: 3517
		private List<IMailboxLocationInfo> newArchiveAndExchangeInfo;

		// Token: 0x04000DBE RID: 3518
		private int[] mailboxTypeCount;

		// Token: 0x04000DBF RID: 3519
		private IPropertyBag propertyBag;

		// Token: 0x04000DC0 RID: 3520
		private bool isInitialGuidsValid;
	}
}
