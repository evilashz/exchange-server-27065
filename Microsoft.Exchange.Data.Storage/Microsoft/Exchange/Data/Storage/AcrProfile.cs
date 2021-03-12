using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE8 RID: 2792
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcrProfile
	{
		// Token: 0x0600652C RID: 25900 RVA: 0x001AD810 File Offset: 0x001ABA10
		internal AcrProfile(AcrPropertyResolverChain.ResolutionFunction genericResolutionFunction, params AcrProfile[] baseProfiles) : this(baseProfiles, new AcrPropertyResolverChain(new AcrPropertyResolverChain.ResolutionFunction[]
		{
			genericResolutionFunction,
			new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToServerValueIfClientMatchesServer)
		}, null, true))
		{
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x001AD848 File Offset: 0x001ABA48
		private AcrProfile(AcrProfile[] baseProfiles, AcrPropertyResolver genericResolver)
		{
			if (baseProfiles != null)
			{
				foreach (AcrProfile acrProfile in baseProfiles)
				{
					int num = 0;
					foreach (AcrProfile acrProfile2 in baseProfiles)
					{
						if (acrProfile == acrProfile2)
						{
							num++;
						}
						else
						{
							foreach (PropertyDefinition key in acrProfile.propertyProfileCollection.Keys)
							{
								if (acrProfile2.propertyProfileCollection.ContainsKey(key))
								{
									throw new ArgumentException(ServerStrings.ExInvalidAcrBaseProfiles);
								}
							}
						}
					}
					if (num != 1)
					{
						throw new ArgumentException(ServerStrings.ExInvalidAcrBaseProfiles);
					}
				}
			}
			this.baseProfiles = baseProfiles;
			this.genericResolver = genericResolver;
		}

		// Token: 0x17001BEE RID: 7150
		internal AcrPropertyProfile this[PropertyDefinition propertyDefinition]
		{
			get
			{
				AcrPropertyProfile acrPropertyProfile;
				if (!this.propertyProfileCollection.TryGetValue(propertyDefinition, out acrPropertyProfile) && this.baseProfiles != null)
				{
					int num = 0;
					while (num < this.baseProfiles.Length && acrPropertyProfile == null)
					{
						acrPropertyProfile = this.baseProfiles[num][propertyDefinition];
						num++;
					}
				}
				return acrPropertyProfile;
			}
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x001AD987 File Offset: 0x001ABB87
		internal static AcrProfile CreateWithGenericResolver(AcrPropertyResolver resolver, params AcrProfile[] baseProfiles)
		{
			return new AcrProfile(baseProfiles, resolver);
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x001AD990 File Offset: 0x001ABB90
		internal HashSet<PropertyDefinition> GetPropertiesNeededForResolution(IEnumerable<PropertyDefinition> propertyDefinitions)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				foreach (PropertyDefinition item in this.GetPropertiesNeededForResolution(propertyDefinition))
				{
					hashSet.TryAdd(item);
				}
			}
			return hashSet;
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x001ADBE4 File Offset: 0x001ABDE4
		internal IEnumerable<PropertyDefinition> GetPropertiesNeededForResolution(PropertyDefinition propertyDefinition)
		{
			AcrPropertyProfile profile = this[propertyDefinition];
			if (profile != null)
			{
				foreach (PropertyDefinition relatedPropertyDefinition in profile.AllProperties)
				{
					yield return relatedPropertyDefinition;
				}
			}
			else
			{
				yield return propertyDefinition;
			}
			yield break;
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x001ADC08 File Offset: 0x001ABE08
		internal ConflictResolutionResult ResolveConflicts(Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> propertyValuesToResolve)
		{
			Dictionary<PropertyDefinition, PropertyConflict> dictionary = new Dictionary<PropertyDefinition, PropertyConflict>(propertyValuesToResolve.Count);
			foreach (KeyValuePair<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> keyValuePair in propertyValuesToResolve)
			{
				AcrPropertyProfile acrPropertyProfile = this[keyValuePair.Key];
				if (!dictionary.ContainsKey(keyValuePair.Key))
				{
					if (acrPropertyProfile != null)
					{
						AcrProfile.ResolveConflicts(dictionary, acrPropertyProfile.Resolver, propertyValuesToResolve, acrPropertyProfile.PropertiesToResolve);
					}
					else
					{
						AcrProfile.ResolveConflicts(dictionary, this.genericResolver, propertyValuesToResolve, new PropertyDefinition[]
						{
							keyValuePair.Key
						});
					}
				}
			}
			SaveResult saveResult = SaveResult.Success;
			foreach (PropertyConflict propertyConflict in dictionary.Values)
			{
				if (!propertyConflict.ConflictResolvable)
				{
					saveResult = SaveResult.IrresolvableConflict;
					break;
				}
				saveResult = SaveResult.SuccessWithConflictResolution;
			}
			return new ConflictResolutionResult(saveResult, Util.CollectionToArray<PropertyConflict>(dictionary.Values));
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x001ADD14 File Offset: 0x001ABF14
		private static AcrPropertyProfile.ValuesToResolve[] FilterValuesToResolve(Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> propertyValuesToResolve, PropertyDefinition[] propertiesToInclude)
		{
			AcrPropertyProfile.ValuesToResolve[] array = new AcrPropertyProfile.ValuesToResolve[propertiesToInclude.Length];
			for (int i = 0; i < propertiesToInclude.Length; i++)
			{
				propertyValuesToResolve.TryGetValue(propertiesToInclude[i], out array[i]);
			}
			return array;
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x001ADD4C File Offset: 0x001ABF4C
		private static void ResolveConflicts(Dictionary<PropertyDefinition, PropertyConflict> conflicts, AcrPropertyResolver resolver, Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> propertyValuesToResolve, PropertyDefinition[] propertiesToResolve)
		{
			AcrPropertyProfile.ValuesToResolve[] array = AcrProfile.FilterValuesToResolve(propertyValuesToResolve, propertiesToResolve);
			AcrPropertyProfile.ValuesToResolve[] dependencies = AcrProfile.FilterValuesToResolve(propertyValuesToResolve, resolver.Dependencies);
			object[] array2 = resolver.Resolve(array, dependencies);
			for (int i = 0; i < propertiesToResolve.Length; i++)
			{
				PropertyConflict value = new PropertyConflict(propertiesToResolve[i], array[i].OriginalValue, array[i].ClientValue, array[i].ServerValue, (array2 != null) ? array2[i] : null, array2 != null);
				conflicts.Add(propertiesToResolve[i], value);
			}
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001ADDC4 File Offset: 0x001ABFC4
		private static AcrProfile CreateBlankProfile()
		{
			return new AcrProfile(null, new AcrProfile[0]);
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001ADDE0 File Offset: 0x001ABFE0
		private static AcrProfile CreateFollowupFlagProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[]
			{
				AcrProfile.ReminderProfile
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToEarlierTime), false, new StorePropertyDefinition[]
			{
				InternalSchema.ReplyTime
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToOredValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.IsReplyRequested
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToOredValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.IsResponseRequested
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestIntValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MapiFlagStatus
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestIntValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.TaskStatus
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.FlagRequest
			});
			return acrProfile;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x001ADEE4 File Offset: 0x001AC0E4
		private static AcrProfile CreateReminderProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[0]);
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToOredValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ReminderIsSetInternal
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToEarlierTime), false, new StorePropertyDefinition[]
			{
				InternalSchema.ReminderDueByInternal
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToEarlierTime), false, new StorePropertyDefinition[]
			{
				InternalSchema.ReminderNextTime
			});
			return acrProfile;
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x001ADF68 File Offset: 0x001AC168
		private static AcrProfile CreateReplyForwardRelatedProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[0]);
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.IconIndex
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.LastVerbExecuted
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.LastVerbExecutionTime
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.Flags
			});
			return acrProfile;
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x001AE014 File Offset: 0x001AC214
		private static AcrProfile CreateCommonMessageProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[0]);
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestPriorityAndImportance), false, new StorePropertyDefinition[]
			{
				InternalSchema.MapiPriority,
				InternalSchema.MapiImportance
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestSensitivity), false, new StorePropertyDefinition[]
			{
				InternalSchema.MapiSensitivity,
				InternalSchema.Privacy
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToMergedStringValues), false, new StorePropertyDefinition[]
			{
				InternalSchema.Categories
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.MapiSubject
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.AppointmentStateInternal
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToModifiedValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ConversationIndexTracking
			});
			return acrProfile;
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x001AE11C File Offset: 0x001AC31C
		private static AcrProfile CreateAppointmentProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[]
			{
				AcrProfile.CommonMessageProfile,
				AcrProfile.ReminderProfile
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestIntValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ReminderMinutesBeforeStartInternal
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.MapiStartTime
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.MapiPRStartDate
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.Location
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.LocationDisplayName
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValueIfServerValueNotModified), true, new StorePropertyDefinition[]
			{
				InternalSchema.LidWhere
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OutlookInternalVersion
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OutlookVersion
			});
			AcrProfile.AddCalendarLoggingPropertyProfile(acrProfile);
			return acrProfile;
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x001AE27C File Offset: 0x001AC47C
		private static AcrProfile CreateContactProfile()
		{
			return new AcrProfile(null, new AcrProfile[]
			{
				AcrProfile.FollowupFlagProfile
			});
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x001AE2A4 File Offset: 0x001AC4A4
		private static AcrProfile CreateMailboxAssociationProfile()
		{
			AcrProfile acrProfile = new AcrProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), new AcrProfile[]
			{
				AcrProfile.BlankProfile
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToIncrementHighestIntValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MailboxAssociationCurrentVersion
			});
			return acrProfile;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x001AE2F8 File Offset: 0x001AC4F8
		private static AcrProfile CreateMessageProfile()
		{
			return new AcrProfile(null, new AcrProfile[]
			{
				AcrProfile.CommonMessageProfile,
				AcrProfile.ReminderProfile,
				AcrProfile.FollowupFlagProfile,
				AcrProfile.ReplyForwardRelatedProfile
			});
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x001AE338 File Offset: 0x001AC538
		private static AcrProfile CreateMeetingMessageProfile()
		{
			AcrProfile acrProfile = new AcrProfile(null, new AcrProfile[]
			{
				AcrProfile.CommonMessageProfile
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToOredValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.IsProcessed
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveResponseType), false, new StorePropertyDefinition[]
			{
				InternalSchema.MapiResponseType
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OutlookInternalVersion
			});
			AcrProfile.AddCalendarLoggingPropertyProfile(acrProfile);
			return acrProfile;
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x001AE3D0 File Offset: 0x001AC5D0
		private static void AddCalendarLoggingPropertyProfile(AcrProfile profile)
		{
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ItemVersion
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ChangeList
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.CalendarLogTriggerAction
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OriginalFolderId
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OriginalCreationTime
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OriginalLastModifiedTime
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.OriginalEntryId
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ClientInfoString
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ClientProcessName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ClientMachineName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ClientBuildVersion
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MiddleTierProcessName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MiddleTierServerName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MiddleTierServerBuildVersion
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MailboxServerName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MailboxServerBuildVersion
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.MailboxDatabaseName
			});
			profile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToClientValue), false, new StorePropertyDefinition[]
			{
				InternalSchema.ResponsibleUserName
			});
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x001AE680 File Offset: 0x001AC880
		private static AcrProfile CreateCategoryProfile()
		{
			AcrProfile acrProfile = AcrProfile.CreateWithGenericResolver(new AcrPropertyResolverChain(new AcrPropertyResolverChain.ResolutionFunction[]
			{
				new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToModifiedValue),
				new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToGreatestDependency<ExDateTime>)
			}, new PropertyDefinition[]
			{
				CategorySchema.LastTimeUsed
			}, false), new AcrProfile[0]);
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsed
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedCalendar
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedContacts
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedJournal
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedMail
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedNotes
			});
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				CategorySchema.LastTimeUsedTasks
			});
			return acrProfile;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x001AE7E0 File Offset: 0x001AC9E0
		private static AcrProfile CreateMasterCategoryListProfile()
		{
			AcrProfile acrProfile = AcrProfile.CreateWithGenericResolver(new AcrPropertyResolverChain(new AcrPropertyResolverChain.ResolutionFunction[]
			{
				new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToModifiedValue),
				new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToGreatestDependency<ExDateTime>)
			}, new PropertyDefinition[]
			{
				MasterCategoryListSchema.LastSavedTime
			}, false), new AcrProfile[0]);
			acrProfile.AddPropertyProfile(new AcrPropertyResolverChain.ResolutionFunction(AcrHelpers.ResolveToHighestValue<ExDateTime>), false, new StorePropertyDefinition[]
			{
				MasterCategoryListSchema.LastSavedTime
			});
			return acrProfile;
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x001AE85C File Offset: 0x001ACA5C
		private void AddPropertyProfile(AcrPropertyResolverChain.ResolutionFunction resolutionFunction, bool requireChangeTracking, params StorePropertyDefinition[] interDependentProperties)
		{
			for (int i = 0; i < interDependentProperties.Length; i++)
			{
				PropertyDefinition propertyDefinition = interDependentProperties[i];
				if (propertyDefinition is SmartPropertyDefinition)
				{
					throw new ArgumentException("interndependentProperties cannot contain SmartProperties");
				}
				if (this.propertyProfileCollection.ContainsKey(propertyDefinition) || i < Array.LastIndexOf<PropertyDefinition>(interDependentProperties, propertyDefinition))
				{
					throw new ArgumentException(ServerStrings.ExPropertyDefinitionInMoreThanOnePropertyProfile);
				}
			}
			AcrPropertyProfile value = new AcrPropertyProfile(new AcrPropertyResolverChain(new AcrPropertyResolverChain.ResolutionFunction[]
			{
				resolutionFunction
			}, null, false), requireChangeTracking, interDependentProperties);
			foreach (StorePropertyDefinition key in interDependentProperties)
			{
				this.propertyProfileCollection.Add(key, value);
			}
		}

		// Token: 0x040039A0 RID: 14752
		private static readonly AcrProfile ReminderProfile = AcrProfile.CreateReminderProfile();

		// Token: 0x040039A1 RID: 14753
		private static readonly AcrProfile FollowupFlagProfile = AcrProfile.CreateFollowupFlagProfile();

		// Token: 0x040039A2 RID: 14754
		private static readonly AcrProfile ReplyForwardRelatedProfile = AcrProfile.CreateReplyForwardRelatedProfile();

		// Token: 0x040039A3 RID: 14755
		private readonly Dictionary<PropertyDefinition, AcrPropertyProfile> propertyProfileCollection = new Dictionary<PropertyDefinition, AcrPropertyProfile>();

		// Token: 0x040039A4 RID: 14756
		private readonly AcrProfile[] baseProfiles;

		// Token: 0x040039A5 RID: 14757
		private readonly AcrPropertyResolver genericResolver;

		// Token: 0x040039A6 RID: 14758
		internal static readonly AcrProfile BlankProfile = AcrProfile.CreateBlankProfile();

		// Token: 0x040039A7 RID: 14759
		internal static readonly AcrProfile GenericItemProfile = AcrProfile.ReminderProfile;

		// Token: 0x040039A8 RID: 14760
		internal static readonly AcrProfile CommonMessageProfile = AcrProfile.CreateCommonMessageProfile();

		// Token: 0x040039A9 RID: 14761
		internal static readonly AcrProfile FolderProfile = AcrProfile.BlankProfile;

		// Token: 0x040039AA RID: 14762
		internal static readonly AcrProfile AppointmentProfile = AcrProfile.CreateAppointmentProfile();

		// Token: 0x040039AB RID: 14763
		internal static readonly AcrProfile MessageProfile = AcrProfile.CreateMessageProfile();

		// Token: 0x040039AC RID: 14764
		internal static readonly AcrProfile MeetingMessageProfile = AcrProfile.CreateMeetingMessageProfile();

		// Token: 0x040039AD RID: 14765
		internal static readonly AcrProfile ContactProfile = AcrProfile.CreateContactProfile();

		// Token: 0x040039AE RID: 14766
		internal static readonly AcrProfile CategoryProfile = AcrProfile.CreateCategoryProfile();

		// Token: 0x040039AF RID: 14767
		internal static readonly AcrProfile MasterCategoryListProfile = AcrProfile.CreateMasterCategoryListProfile();

		// Token: 0x040039B0 RID: 14768
		internal static readonly AcrProfile MailboxAssociationProfile = AcrProfile.CreateMailboxAssociationProfile();
	}
}
