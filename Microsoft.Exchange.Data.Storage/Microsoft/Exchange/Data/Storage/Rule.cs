using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BB8 RID: 3000
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Rule
	{
		// Token: 0x06006B20 RID: 27424 RVA: 0x001C882C File Offset: 0x001C6A2C
		private Rule(Folder folder, Rule serverRule)
		{
			this.serverRule = serverRule;
			this.folder = folder;
			this.name = string.Empty;
			this.ruleId = null;
			this.isEnabled = true;
			this.sequence = 0;
			this.provider = "RuleOrganizer";
			this.providerId = Rule.ProviderIdEnum.OL98Plus;
			this.onlyOof = false;
			this.isInError = false;
			this.isNotSupported = false;
			this.actions = new ActionList(this);
			this.conditions = new ConditionList(this);
			this.exceptions = new ConditionList(this);
			this.toParticipants = new List<Participant>();
			this.fromParticipants = new List<Participant>();
			this.containsSubjectStrings = new List<string>();
			this.containsBodyStrings = new List<string>();
			this.containsSubjectOrBodyStrings = new List<string>();
			this.containsSenderStrings = new List<string>();
			this.containsHeaderStrings = new List<string>();
			this.containsRecipientStrings = new List<string>();
			this.assignedCategoriesStrings = new List<string>();
			this.messageClassificationStrings = new List<string>();
			this.exceptToParticipants = new List<Participant>();
			this.exceptFromParticipants = new List<Participant>();
			this.exceptSubjectStrings = new List<string>();
			this.exceptBodyStrings = new List<string>();
			this.exceptSubjectOrBodyStrings = new List<string>();
			this.exceptSenderStrings = new List<string>();
			this.exceptRecipientStrings = new List<string>();
			this.exceptHeaderStrings = new List<string>();
			this.exceptCategoriesStrings = new List<string>();
			this.exceptMessageClassificationStrings = new List<string>();
			if (serverRule != null)
			{
				this.ParseServerRule();
				this.isNew = false;
				this.ClearDirty();
				return;
			}
			this.isNew = true;
			this.SetDirty();
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x001C8A26 File Offset: 0x001C6C26
		public static Rule Create(Rules rules)
		{
			return new Rule(rules.Folder, null);
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x001C8A34 File Offset: 0x001C6C34
		internal static Rule Create(Folder folder, Rule serverRule)
		{
			return new Rule(folder, serverRule);
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06006B23 RID: 27427 RVA: 0x001C8A3D File Offset: 0x001C6C3D
		// (set) Token: 0x06006B24 RID: 27428 RVA: 0x001C8A45 File Offset: 0x001C6C45
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Name");
				}
				this.name = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06006B25 RID: 27429 RVA: 0x001C8A62 File Offset: 0x001C6C62
		// (set) Token: 0x06006B26 RID: 27430 RVA: 0x001C8A6A File Offset: 0x001C6C6A
		public RuleId Id
		{
			get
			{
				return this.ruleId;
			}
			internal set
			{
				this.ruleId = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06006B27 RID: 27431 RVA: 0x001C8A79 File Offset: 0x001C6C79
		// (set) Token: 0x06006B28 RID: 27432 RVA: 0x001C8A81 File Offset: 0x001C6C81
		public string Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Provider");
				}
				this.provider = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06006B29 RID: 27433 RVA: 0x001C8A9E File Offset: 0x001C6C9E
		// (set) Token: 0x06006B2A RID: 27434 RVA: 0x001C8AA6 File Offset: 0x001C6CA6
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06006B2B RID: 27435 RVA: 0x001C8AB5 File Offset: 0x001C6CB5
		// (set) Token: 0x06006B2C RID: 27436 RVA: 0x001C8ABD File Offset: 0x001C6CBD
		public bool RunOnlyWhileOof
		{
			get
			{
				return this.onlyOof;
			}
			set
			{
				this.onlyOof = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06006B2D RID: 27437 RVA: 0x001C8ACC File Offset: 0x001C6CCC
		// (set) Token: 0x06006B2E RID: 27438 RVA: 0x001C8AD4 File Offset: 0x001C6CD4
		public bool IsParameterInError
		{
			get
			{
				return this.isInError;
			}
			set
			{
				this.isInError = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06006B2F RID: 27439 RVA: 0x001C8AE3 File Offset: 0x001C6CE3
		public bool IsNotSupported
		{
			get
			{
				return this.isNotSupported;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06006B30 RID: 27440 RVA: 0x001C8AEB File Offset: 0x001C6CEB
		public IList<ActionBase> Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06006B31 RID: 27441 RVA: 0x001C8AF3 File Offset: 0x001C6CF3
		public IList<Condition> Conditions
		{
			get
			{
				return this.conditions;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06006B32 RID: 27442 RVA: 0x001C8AFB File Offset: 0x001C6CFB
		public IList<Condition> Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06006B33 RID: 27443 RVA: 0x001C8B03 File Offset: 0x001C6D03
		public QueryFilter ConditionFilter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x06006B34 RID: 27444 RVA: 0x001C8B0A File Offset: 0x001C6D0A
		// (set) Token: 0x06006B35 RID: 27445 RVA: 0x001C8B12 File Offset: 0x001C6D12
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Sequence");
				}
				this.sequence = value;
				this.SetDirty();
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06006B36 RID: 27446 RVA: 0x001C8B30 File Offset: 0x001C6D30
		public bool IsNew
		{
			get
			{
				return this.isNew;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06006B37 RID: 27447 RVA: 0x001C8B38 File Offset: 0x001C6D38
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06006B38 RID: 27448 RVA: 0x001C8B40 File Offset: 0x001C6D40
		// (set) Token: 0x06006B39 RID: 27449 RVA: 0x001C8B48 File Offset: 0x001C6D48
		public Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return this.providerId;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<Rule.ProviderIdEnum>(value);
				this.providerId = value;
			}
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x001C8B58 File Offset: 0x001C6D58
		public void Save()
		{
			if (this.IsNotSupported)
			{
				throw new NotSupportedException();
			}
			if (this.IsNew)
			{
				this.serverRule = new Rule();
				this.serverRule.Operation = RuleOperation.Create;
			}
			else if (this.isDirty)
			{
				this.serverRule.Operation = RuleOperation.Update;
			}
			Rule.ProviderData providerData;
			providerData.Version = 1U;
			providerData.RuleSearchKey = 0U;
			providerData.TimeStamp = ExDateTime.UtcNow.ToFileTime();
			this.serverRule.ProviderData = providerData.ToByteArray();
			this.serverRule.ExecutionSequence = this.Sequence;
			this.serverRule.Name = this.Name;
			this.serverRule.Provider = this.Provider;
			FromRecipientsCondition fromRecipientsCondition = null;
			for (int i = 0; i < this.Exceptions.Count; i++)
			{
				if (this.Exceptions[i].ConditionType == ConditionType.FromRecipientsCondition)
				{
					fromRecipientsCondition = (FromRecipientsCondition)this.Exceptions[i];
					this.Exceptions.RemoveAt(i);
					break;
				}
			}
			Condition[] array = this.conditions.ToArray();
			Condition[] array2 = this.exceptions.ToArray();
			Array.Sort(array, Condition.ConditionOrderComparer);
			Array.Sort(array2, Condition.ExceptionOrderComparer);
			List<Restriction> list = new List<Restriction>();
			if (fromRecipientsCondition != null)
			{
				list.Add(Restriction.Not(fromRecipientsCondition.BuildRestriction()));
			}
			for (int j = 0; j < array.Length; j++)
			{
				Restriction restriction = array[j].BuildRestriction();
				if (restriction != null)
				{
					list.Add(restriction);
				}
			}
			for (int k = 0; k < array2.Length; k++)
			{
				Restriction restriction2 = array2[k].BuildRestriction();
				if (restriction2 != null)
				{
					list.Add(Restriction.Not(restriction2));
				}
			}
			Restriction condition;
			if (list.Count == 0)
			{
				condition = Restriction.Exist(PropTag.MessageClass);
			}
			else if (list.Count > 1)
			{
				condition = Restriction.And(list.ToArray());
			}
			else
			{
				condition = list[0];
			}
			this.serverRule.Condition = condition;
			uint num = (uint)this.serverRule.StateFlags;
			num &= 4294967279U;
			ActionBase[] array3 = this.actions.ToArray();
			Array.Sort(array3, ActionBase.ActionOrderComparer);
			List<RuleAction> list2 = new List<RuleAction>();
			for (int l = 0; l < array3.Length; l++)
			{
				RuleAction ruleAction = array3[l].BuildRuleAction();
				if (ruleAction != null)
				{
					list2.Add(ruleAction);
				}
				if (array3[l].ActionType == ActionType.DeleteAction || array3[l].ActionType == ActionType.PermanentDeleteAction || ((IList<ActionBase>)this.actions)[l].ActionType == ActionType.StopProcessingAction)
				{
					num |= 16U;
				}
			}
			this.serverRule.Actions = list2.ToArray();
			if (this.IsEnabled)
			{
				num |= 1U;
			}
			else
			{
				num &= 4294967294U;
			}
			if (this.RunOnlyWhileOof)
			{
				num |= 4U;
			}
			else
			{
				num &= 4294967291U;
			}
			if (this.isInError)
			{
				num |= 2U;
			}
			else
			{
				num &= 4294967293U;
			}
			this.serverRule.StateFlags = (RuleStateFlags)num;
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x001C8E4C File Offset: 0x001C704C
		public void SaveNotSupported()
		{
			if (!this.isNotSupported)
			{
				throw new InvalidOperationException("SaveNotSupported() should only be called on a not supported rule.");
			}
			if (this.isNew)
			{
				throw new InvalidOperationException("Not supported rule cannot be created.");
			}
			if (this.serverRule == null)
			{
				throw new InvalidOperationException("The not supported rule has not been loaded.");
			}
			if (this.isDirty)
			{
				this.serverRule.ExecutionSequence = this.sequence;
				this.serverRule.Name = this.name;
				if (this.isEnabled)
				{
					this.serverRule.StateFlags |= RuleStateFlags.Enabled;
				}
				else
				{
					this.serverRule.StateFlags &= ~RuleStateFlags.Enabled;
				}
				this.serverRule.Operation = RuleOperation.Update;
			}
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x001C8EF9 File Offset: 0x001C70F9
		public void MarkDelete()
		{
			if (this.isNew)
			{
				throw new InvalidOperationException("Cannot delete a new rule that does not exist in store yet.");
			}
			if (this.serverRule == null)
			{
				throw new InvalidOperationException("Cannot delete a rule that does not exist in store or has not been loaded from store yet.");
			}
			this.serverRule.Operation = RuleOperation.Delete;
			this.SetDirty();
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x001C8F33 File Offset: 0x001C7133
		private static void ThrowRuleParseIfNull(object value)
		{
			if (value == null)
			{
				throw new RuleParseException(ServerStrings.UnsupportedPropertyRestriction);
			}
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x001C8F48 File Offset: 0x001C7148
		private static void ThrowRuleParseIfNullOrEmpty(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new RuleParseException(ServerStrings.UnsupportedPropertyRestriction);
			}
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x001C8F64 File Offset: 0x001C7164
		private static bool IsSenderRst(Restriction res)
		{
			Restriction.CommentRestriction commentRestriction = res as Restriction.CommentRestriction;
			if (commentRestriction == null)
			{
				throw new ArgumentException();
			}
			Restriction.PropertyRestriction propertyRestriction;
			if (commentRestriction.Restriction is Restriction.OrRestriction)
			{
				Restriction.OrRestriction orRestriction = commentRestriction.Restriction as Restriction.OrRestriction;
				propertyRestriction = (orRestriction.Restrictions[0] as Restriction.PropertyRestriction);
			}
			else
			{
				propertyRestriction = (commentRestriction.Restriction as Restriction.PropertyRestriction);
			}
			PropTag propTag = propertyRestriction.PropTag;
			if (propTag == PropTag.SenderSearchKey || propTag == PropTag.SenderEntryId)
			{
				return true;
			}
			if (propTag == PropTag.SearchKey || propTag == (PropTag)65794U)
			{
				return false;
			}
			throw new ArgumentException();
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x001C8FE8 File Offset: 0x001C71E8
		private static void ParticipantFromRestriction(Restriction res, IList<Participant> participants)
		{
			if (res is Restriction.ContentRestriction)
			{
				Restriction.ContentRestriction contentRestriction = res as Restriction.ContentRestriction;
				string displayName = (string)contentRestriction.PropValue.Value;
				participants.Add(new Participant(displayName, null, null));
				return;
			}
			if (res is Restriction.CommentRestriction)
			{
				Restriction.CommentRestriction commentRestriction = res as Restriction.CommentRestriction;
				PropValue[] values = commentRestriction.Values;
				object obj = null;
				if (commentRestriction.Restriction is Restriction.PropertyRestriction)
				{
					Restriction.PropertyRestriction propertyRestriction = commentRestriction.Restriction as Restriction.PropertyRestriction;
					obj = propertyRestriction.PropValue.Value;
				}
				else
				{
					Restriction.OrRestriction orRestriction = commentRestriction.Restriction as Restriction.OrRestriction;
					for (long num = 0L; num < (long)orRestriction.Restrictions.Length; num += 1L)
					{
						Restriction.PropertyRestriction propertyRestriction = orRestriction.Restrictions[(int)(checked((IntPtr)num))] as Restriction.PropertyRestriction;
						if (propertyRestriction.PropTag == PropTag.SenderSearchKey)
						{
							obj = propertyRestriction.PropValue.Value;
							break;
						}
					}
				}
				participants.Add(Rule.CreateParticipant((string)values[2].Value, (byte[])values[1].Value, (byte[])obj));
			}
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x001C9104 File Offset: 0x001C7304
		private static Participant CreateParticipant(string displayName, byte[] entryIdBytes, byte[] searchKey)
		{
			Participant.Builder builder = new Participant.Builder();
			ParticipantEntryId participantEntryId = null;
			if (entryIdBytes != null)
			{
				participantEntryId = ParticipantEntryId.TryFromEntryId(entryIdBytes);
				if (participantEntryId != null)
				{
					builder.SetPropertiesFrom(participantEntryId);
				}
			}
			if (displayName != null)
			{
				builder.DisplayName = displayName;
			}
			if (builder.DisplayName == null)
			{
				builder.DisplayName = ClientStrings.UnknownDelegateUser;
			}
			StoreParticipantEntryId storeParticipantEntryId = participantEntryId as StoreParticipantEntryId;
			if (storeParticipantEntryId != null)
			{
				if (storeParticipantEntryId.IsDL == true)
				{
					throw new RuleParseException(ServerStrings.NoMapiPDLs);
				}
				if (searchKey != null)
				{
					string text = null;
					string text2 = null;
					Rule.ParseSearchKey(searchKey, out text, out text2);
					if (text != null)
					{
						builder.RoutingType = text;
					}
					if (text2 != null)
					{
						builder.EmailAddress = text2;
					}
				}
			}
			return builder.ToParticipant();
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x001C91B8 File Offset: 0x001C73B8
		private static string ConvertSearchKeyValue(byte[] value)
		{
			string text = new string(CTSGlobals.AsciiEncoding.GetChars(value, 0, value.Length));
			string text2 = text;
			char[] trimChars = new char[1];
			return text2.TrimEnd(trimChars);
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x001C91EC File Offset: 0x001C73EC
		private static void ParseSearchKey(byte[] searchKeyBytes, out string routingType, out string emailaddress)
		{
			string text = Rule.ConvertSearchKeyValue(searchKeyBytes);
			string[] array = text.Split(new char[]
			{
				':'
			}, 2);
			if (array.Length >= 2)
			{
				routingType = array[0];
				emailaddress = array[1];
				return;
			}
			routingType = null;
			emailaddress = null;
		}

		// Token: 0x06006B44 RID: 27460 RVA: 0x001C922C File Offset: 0x001C742C
		private static void CrackAddressList(IList<Participant> participants, params Restriction[] resArray)
		{
			for (int i = 0; i < resArray.Length; i++)
			{
				Rule.ParticipantFromRestriction(resArray[i], participants);
			}
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x001C9250 File Offset: 0x001C7450
		private static byte[] SearchKeyFromParticipant(Participant participant)
		{
			if (participant.EmailAddress == null || participant.RoutingType == null)
			{
				throw new InvalidParticipantException(ServerStrings.InvalidParticipantForRules, ParticipantValidationStatus.AddressAndRoutingTypeMismatch);
			}
			if (participant.EmailAddress.Length == 0 || participant.RoutingType.Length == 0)
			{
				throw new InvalidParticipantException(ServerStrings.InvalidParticipantForRules, ParticipantValidationStatus.AddressAndRoutingTypeMismatch);
			}
			if (participant.RoutingType == "MAPIPDL")
			{
				ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.RecipientTableSecondary);
				return participantEntryId.ToByteArray();
			}
			string text = participant.RoutingType + ":" + participant.EmailAddress + "\0";
			text = text.ToUpperInvariant();
			return CTSGlobals.AsciiEncoding.GetBytes(text);
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x001C92F4 File Offset: 0x001C74F4
		private static Restriction CommentSearchKey(PropTag tagProp, byte[] searchKey, byte[] entryId, string displayName, LegacyRecipientDisplayType displayType, Restriction.RelOp relOp)
		{
			PropValue[] propValues = new PropValue[]
			{
				new PropValue((PropTag)1610612739U, Rule.InboxSpecialComment.Resolved),
				new PropValue((PropTag)65794U, entryId),
				new PropValue((PropTag)65567U, displayName),
				new PropValue(PropTag.DisplayType, displayType)
			};
			Restriction restriction;
			if (relOp == Restriction.RelOp.MemberOfDL)
			{
				restriction = Restriction.Or(new Restriction[]
				{
					Restriction.EQ(PropTag.SenderSearchKey, searchKey),
					Restriction.MemberOf(PropTag.SenderEntryId, entryId)
				});
			}
			else
			{
				restriction = Restriction.EQ(tagProp, searchKey);
			}
			return Restriction.Comment(restriction, propValues);
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x001C93B4 File Offset: 0x001C75B4
		private static Restriction ContentOrPropertyFromParticipant(Participant participant, PropTag tagDisplayName)
		{
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.RecipientTableSecondary);
			if (participantEntryId == null && participant.DisplayName != null)
			{
				return Restriction.Content(tagDisplayName, participant.DisplayName, ContentFlags.SubString | ContentFlags.IgnoreCase | ContentFlags.Loose);
			}
			Restriction.RelOp relOp = Restriction.RelOp.Equal;
			PropTag propTag = (tagDisplayName == PropTag.SenderName) ? PropTag.SenderSearchKey : PropTag.SearchKey;
			byte[] entryId = participantEntryId.ToByteArray();
			LegacyRecipientDisplayType displayType = (participantEntryId.IsDL == true) ? LegacyRecipientDisplayType.DistributionList : LegacyRecipientDisplayType.MailUser;
			if (propTag == PropTag.SenderSearchKey && participantEntryId.IsDL == true)
			{
				relOp = Restriction.RelOp.MemberOfDL;
				propTag = PropTag.SenderEntryId;
			}
			byte[] searchKey = Rule.SearchKeyFromParticipant(participant);
			return Rule.CommentSearchKey(propTag, searchKey, entryId, participant.DisplayName, displayType, relOp);
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x001C9474 File Offset: 0x001C7674
		private NativeStorePropertyDefinition PropTagToPropertyDefinitionFromCache(PropTag propTag)
		{
			NativeStorePropertyDefinition[] array = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, this.folder.MapiFolder, this.folder.Session, new PropTag[]
			{
				propTag
			});
			if (array == null || array.Length == 0)
			{
				throw new RuleParseException(ServerStrings.MapiCannotGetNamedProperties);
			}
			return array[0];
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x001C94CC File Offset: 0x001C76CC
		private bool IsNdrRestrictionSet(Restriction.AndRestriction resAnd)
		{
			if (2 != resAnd.Restrictions.Length)
			{
				return false;
			}
			Restriction.ContentRestriction contentRestriction = resAnd.Restrictions[0] as Restriction.ContentRestriction;
			if (contentRestriction == null || PropTag.MessageClass != contentRestriction.PropTag)
			{
				return false;
			}
			string value = contentRestriction.PropValue.Value as string;
			if (!"REPORT".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (ContentFlags.Prefix != (ContentFlags.Prefix & contentRestriction.Flags))
			{
				return false;
			}
			contentRestriction = (resAnd.Restrictions[1] as Restriction.ContentRestriction);
			if (contentRestriction == null || PropTag.MessageClass != contentRestriction.PropTag)
			{
				return false;
			}
			value = (contentRestriction.PropValue.Value as string);
			string text = ".NDR";
			return text.Equals(value, StringComparison.OrdinalIgnoreCase) && ContentFlags.SubString == (ContentFlags.SubString & contentRestriction.Flags);
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x001C958C File Offset: 0x001C778C
		private bool IsFlaggedRestrictionSet(Restriction.AndRestriction resAnd)
		{
			if (2 != resAnd.Restrictions.Length)
			{
				return false;
			}
			Restriction.PropertyRestriction propertyRestriction = resAnd.Restrictions[0] as Restriction.PropertyRestriction;
			if (propertyRestriction == null || (PropTag)277872643U != propertyRestriction.PropTag)
			{
				return false;
			}
			if (propertyRestriction.PropValue.GetInt() != 2)
			{
				return false;
			}
			Restriction.PropertyRestriction propertyRestriction2 = resAnd.Restrictions[1] as Restriction.PropertyRestriction;
			if (propertyRestriction2 != null)
			{
				GuidIdPropertyDefinition guidIdPropertyDefinition = this.PropTagToPropertyDefinitionFromCache(propertyRestriction2.PropTag) as GuidIdPropertyDefinition;
				return guidIdPropertyDefinition != null && guidIdPropertyDefinition.Equals(ItemSchema.FlagRequest);
			}
			return false;
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x001C9614 File Offset: 0x001C7814
		private void AddElementFromProp(PropTag propTag, object value, int op, bool isException)
		{
			Rule.ThrowRuleParseIfNull(value);
			if (propTag <= PropTag.TransportMessageHeaders)
			{
				if (propTag <= PropTag.Sensitivity)
				{
					if (propTag <= PropTag.Importance)
					{
						if (propTag != PropTag.AutoForwarded)
						{
							if (propTag != PropTag.Importance)
							{
								goto IL_51D;
							}
							if (isException)
							{
								this.Exceptions.Add(MarkedAsImportanceCondition.Create(this, (Importance)value));
								return;
							}
							this.Conditions.Add(MarkedAsImportanceCondition.Create(this, (Importance)value));
							return;
						}
						else
						{
							if (isException)
							{
								this.Exceptions.Add(AutomaticForwardCondition.Create(this));
								return;
							}
							this.Conditions.Add(AutomaticForwardCondition.Create(this));
							return;
						}
					}
					else if (propTag != PropTag.MessageClass)
					{
						if (propTag != PropTag.Sensitivity)
						{
							goto IL_51D;
						}
						if (isException)
						{
							this.Exceptions.Add(MarkedAsSensitivityCondition.Create(this, (Sensitivity)value));
							return;
						}
						this.Conditions.Add(MarkedAsSensitivityCondition.Create(this, (Sensitivity)value));
						return;
					}
					else
					{
						string value2 = value as string;
						Rule.ThrowRuleParseIfNullOrEmpty(value2);
						if (!"IPM.Note.Rules.OofTemplate.Microsoft".Equals(value2, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
						if (isException)
						{
							this.Exceptions.Add(MarkedAsOofCondition.Create(this));
							return;
						}
						this.Conditions.Add(MarkedAsOofCondition.Create(this));
						return;
					}
				}
				else if (propTag <= PropTag.MessageToMe)
				{
					if (propTag != PropTag.Subject)
					{
						if (propTag != PropTag.MessageToMe)
						{
							goto IL_51D;
						}
						if (isException)
						{
							if (!(bool)value)
							{
								this.Exceptions.Add(NotSentToMeCondition.Create(this));
								return;
							}
							this.Exceptions.Add(SentToMeCondition.Create(this));
							return;
						}
						else
						{
							if (!(bool)value)
							{
								this.Conditions.Add(NotSentToMeCondition.Create(this));
								return;
							}
							this.Conditions.Add(SentToMeCondition.Create(this));
							return;
						}
					}
				}
				else if (propTag != PropTag.MessageCcMe)
				{
					if (propTag != PropTag.MessageRecipMe)
					{
						if (propTag != PropTag.TransportMessageHeaders)
						{
							goto IL_51D;
						}
						string text = value as string;
						Rule.ThrowRuleParseIfNull(text);
						if (isException)
						{
							this.exceptHeaderStrings.Add(text);
							return;
						}
						this.containsHeaderStrings.Add(text);
						return;
					}
					else
					{
						if (isException)
						{
							this.Exceptions.Add(SentToOrCcMeCondition.Create(this));
							return;
						}
						this.Conditions.Add(SentToOrCcMeCondition.Create(this));
						return;
					}
				}
				else
				{
					if (isException)
					{
						this.Exceptions.Add(SentCcMeCondition.Create(this));
						return;
					}
					this.Conditions.Add(SentCcMeCondition.Create(this));
					return;
				}
			}
			else if (propTag <= PropTag.MessageSize)
			{
				if (propTag <= PropTag.SenderName)
				{
					if (propTag != PropTag.RecipientType && propTag != PropTag.SenderName)
					{
						goto IL_51D;
					}
					return;
				}
				else if (propTag != PropTag.SenderSearchKey)
				{
					if (propTag != PropTag.MessageDeliveryTime)
					{
						if (propTag != PropTag.MessageSize)
						{
							goto IL_51D;
						}
						if (op == 3 || op == 2)
						{
							if (isException)
							{
								this.exceptLowRange = new int?(((int)value + 1023) / 1024);
								return;
							}
							this.lowRange = new int?(((int)value + 1023) / 1024);
							return;
						}
						else
						{
							if (op != 1 && op != 0)
							{
								return;
							}
							if (isException)
							{
								this.exceptHighRange = new int?((int)value / 1024);
								return;
							}
							this.highRange = new int?((int)value / 1024);
							return;
						}
					}
					else
					{
						ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, (DateTime)value);
						exDateTime = this.Folder.Session.ExTimeZone.ConvertDateTime(exDateTime);
						if (op == 3 || op == 2)
						{
							if (isException)
							{
								this.exceptAfter = new ExDateTime?(exDateTime);
								return;
							}
							this.after = new ExDateTime?(exDateTime);
							return;
						}
						else
						{
							if (op != 1 && op != 0)
							{
								return;
							}
							if (isException)
							{
								this.exceptBefore = new ExDateTime?(exDateTime);
								return;
							}
							this.before = new ExDateTime?(exDateTime);
							return;
						}
					}
				}
				else
				{
					if (op != 1)
					{
						return;
					}
					byte[] value3 = value as byte[];
					Rule.ThrowRuleParseIfNull(value3);
					string item = Rule.ConvertSearchKeyValue(value3);
					if (isException)
					{
						this.exceptSenderStrings.Add(item);
						return;
					}
					this.containsSenderStrings.Add(item);
					return;
				}
			}
			else if (propTag <= PropTag.Body)
			{
				if (propTag != PropTag.NormalizedSubject)
				{
					if (propTag != PropTag.Body)
					{
						goto IL_51D;
					}
					string text2 = value as string;
					Rule.ThrowRuleParseIfNull(text2);
					if (isException)
					{
						this.exceptBodyStrings.Add(text2);
						return;
					}
					this.containsBodyStrings.Add(text2);
					return;
				}
			}
			else if (propTag != (PropTag)277872643U)
			{
				if (propTag == PropTag.DisplayName)
				{
					return;
				}
				if (propTag != PropTag.SearchKey)
				{
					goto IL_51D;
				}
				if (op != 1)
				{
					return;
				}
				byte[] value4 = value as byte[];
				Rule.ThrowRuleParseIfNull(value4);
				string item2 = Rule.ConvertSearchKeyValue(value4);
				if (isException)
				{
					this.exceptRecipientStrings.Add(item2);
					return;
				}
				this.containsRecipientStrings.Add(item2);
				return;
			}
			else
			{
				if ((int)value != 2)
				{
					return;
				}
				string action = RequestedAction.Any.ToString();
				if (isException)
				{
					this.Exceptions.Add(FlaggedForActionCondition.Create(this, action));
					return;
				}
				this.Conditions.Add(FlaggedForActionCondition.Create(this, action));
				return;
			}
			string text3 = value as string;
			Rule.ThrowRuleParseIfNull(text3);
			if (isException)
			{
				this.exceptSubjectStrings.Add(text3);
				return;
			}
			this.containsSubjectStrings.Add(text3);
			return;
			IL_51D:
			NativeStorePropertyDefinition nativeStorePropertyDefinition = this.PropTagToPropertyDefinitionFromCache(propTag);
			GuidNamePropertyDefinition guidNamePropertyDefinition = nativeStorePropertyDefinition as GuidNamePropertyDefinition;
			if (guidNamePropertyDefinition != null)
			{
				if (guidNamePropertyDefinition.Equals(ItemSchema.Categories))
				{
					string text4 = value as string;
					Rule.ThrowRuleParseIfNull(text4);
					if (isException)
					{
						this.exceptCategoriesStrings.Add(text4);
						return;
					}
					this.assignedCategoriesStrings.Add(text4);
					return;
				}
				else if (guidNamePropertyDefinition.Equals(MessageItemSchema.IsReadReceipt))
				{
					bool flag = (bool)value;
					if (!flag)
					{
						return;
					}
					if (isException)
					{
						this.Exceptions.Add(ReadReceiptCondition.Create(this));
						return;
					}
					this.Conditions.Add(ReadReceiptCondition.Create(this));
					return;
				}
				else if (guidNamePropertyDefinition.Equals(MessageItemSchema.IsSigned))
				{
					bool flag2 = (bool)value;
					if (!flag2)
					{
						return;
					}
					if (isException)
					{
						this.Exceptions.Add(SignedCondition.Create(this));
						return;
					}
					this.Conditions.Add(SignedCondition.Create(this));
					return;
				}
			}
			else
			{
				GuidIdPropertyDefinition guidIdPropertyDefinition = nativeStorePropertyDefinition as GuidIdPropertyDefinition;
				if (guidIdPropertyDefinition != null)
				{
					if (guidIdPropertyDefinition.Equals(ItemSchema.IsClassified))
					{
						if (isException)
						{
							this.isMessageClassificationException = true;
							return;
						}
						this.isMessageClassificationCondition = true;
						return;
					}
					else if (guidIdPropertyDefinition.Equals(ItemSchema.ClassificationGuid))
					{
						string text5 = value as string;
						Rule.ThrowRuleParseIfNull(text5);
						if (isException)
						{
							this.exceptMessageClassificationStrings.Add(text5);
							return;
						}
						this.messageClassificationStrings.Add(text5);
						return;
					}
					else if (guidIdPropertyDefinition.Equals(ItemSchema.RequestedAction))
					{
						RequestedAction requestedAction = (RequestedAction)((int)value);
						string action2 = (requestedAction == RequestedAction.Any) ? RequestedAction.Any.ToString() : LocalizedDescriptionAttribute.FromEnum(FlaggedForActionCondition.RequestedActionType, requestedAction);
						if (isException)
						{
							this.Exceptions.Add(FlaggedForActionCondition.Create(this, action2));
							return;
						}
						this.Conditions.Add(FlaggedForActionCondition.Create(this, action2));
						return;
					}
					else if (guidIdPropertyDefinition.Equals(MessageItemSchema.SharingInstanceGuid))
					{
						Guid guid = (Guid)value;
						if (isException)
						{
							this.Exceptions.Add(FromSubscriptionCondition.Create(this, new Guid[]
							{
								guid
							}));
							return;
						}
						this.Conditions.Add(FromSubscriptionCondition.Create(this, new Guid[]
						{
							guid
						}));
						return;
					}
				}
			}
			throw new RuleParseException(ServerStrings.UnsupportedPropertyRestriction);
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x001C9D78 File Offset: 0x001C7F78
		private void AddSenderElement(bool isException, IList<Participant> participants)
		{
			foreach (Participant item in participants)
			{
				if (isException)
				{
					this.exceptFromParticipants.Add(item);
				}
				else
				{
					this.fromParticipants.Add(item);
				}
			}
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x001C9DD8 File Offset: 0x001C7FD8
		private void AddRecipientElement(bool isException, IList<Participant> participants)
		{
			foreach (Participant item in participants)
			{
				if (isException)
				{
					this.exceptToParticipants.Add(item);
				}
				else
				{
					this.toParticipants.Add(item);
				}
			}
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x001C9E38 File Offset: 0x001C8038
		private void AddElementFromBitmaskProp(PropTag propTag, int mask, Restriction.RelBmr relation, bool isException)
		{
			if (propTag != PropTag.MessageFlags)
			{
				return;
			}
			if ((mask & 16) > 0)
			{
				if (relation == Restriction.RelBmr.NotEqualToZero)
				{
					if (isException)
					{
						this.Exceptions.Add(HasAttachmentCondition.Create(this));
						return;
					}
					this.Conditions.Add(HasAttachmentCondition.Create(this));
					return;
				}
				else
				{
					if (isException)
					{
						this.Conditions.Add(HasAttachmentCondition.Create(this));
						return;
					}
					this.Exceptions.Add(HasAttachmentCondition.Create(this));
				}
			}
		}

		// Token: 0x06006B4F RID: 27471 RVA: 0x001C9EAC File Offset: 0x001C80AC
		private void ParseOrRestriction(Restriction.OrRestriction res, bool isException)
		{
			Restriction[] restrictions = res.Restrictions;
			int num = res.Restrictions.Length;
			if (num == 0)
			{
				return;
			}
			if (!(restrictions[0] is Restriction.ContentRestriction))
			{
				if (restrictions[0] is Restriction.CommentRestriction)
				{
					List<Participant> participants = new List<Participant>();
					Rule.CrackAddressList(participants, restrictions);
					try
					{
						if (Rule.IsSenderRst(restrictions[0]))
						{
							this.AddSenderElement(isException, participants);
						}
						else
						{
							this.AddRecipientElement(isException, participants);
						}
						return;
					}
					catch (ArgumentException)
					{
						throw new RuleParseException(ServerStrings.MalformedCommentRestriction);
					}
				}
				if (restrictions[0] is Restriction.PropertyRestriction)
				{
					List<string> list = new List<string>();
					List<Guid> list2 = new List<Guid>();
					int num2 = 0;
					int num3 = 0;
					PropTag propTag = this.PropertyDefinitionToPropTagFromCache(InternalSchema.SharingInstanceGuid);
					for (int i = 0; i < num; i++)
					{
						Restriction.PropertyRestriction propertyRestriction = restrictions[i] as Restriction.PropertyRestriction;
						PropTag propTag2 = propertyRestriction.PropTag;
						if (propTag2 == PropTag.MessageClass)
						{
							list.Add((string)propertyRestriction.PropValue.Value);
						}
						else if (propTag2 == PropTag.MessageCcMe)
						{
							num2++;
						}
						else if (propTag2 == PropTag.MessageToMe)
						{
							num3++;
						}
						else if (propTag2 == propTag)
						{
							list2.Add((Guid)propertyRestriction.PropValue.Value);
						}
					}
					if (num == list.Count)
					{
						if (isException)
						{
							this.Exceptions.Add(FormsCondition.Create(ConditionType.FormsCondition, this, list.ToArray()));
							return;
						}
						this.Conditions.Add(FormsCondition.Create(ConditionType.FormsCondition, this, list.ToArray()));
						return;
					}
					else if (num2 > 0 && num3 > 0)
					{
						if (isException)
						{
							this.Exceptions.Add(SentToOrCcMeCondition.Create(this));
							return;
						}
						this.Conditions.Add(SentToOrCcMeCondition.Create(this));
						return;
					}
					else if (num == list2.Count)
					{
						if (isException)
						{
							this.Exceptions.Add(FromSubscriptionCondition.Create(this, list2.ToArray()));
							return;
						}
						this.Conditions.Add(FromSubscriptionCondition.Create(this, list2.ToArray()));
						return;
					}
				}
				else
				{
					for (int j = 0; j < num; j++)
					{
						this.AddRestrictions(restrictions[j], isException);
					}
				}
				return;
			}
			List<string> list3 = new List<string>();
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int k = 0;
			while (k < num)
			{
				Restriction.ContentRestriction contentRestriction = restrictions[k] as Restriction.ContentRestriction;
				if (contentRestriction == null)
				{
					throw new RuleParseException(ServerStrings.UnsupportedContentRestriction);
				}
				PropTag propTag2 = contentRestriction.PropTag;
				PropTag propTag3 = propTag2;
				if (propTag3 <= PropTag.SenderName)
				{
					if (propTag3 <= PropTag.Subject)
					{
						if (propTag3 != PropTag.MessageClass)
						{
							if (propTag3 == PropTag.Subject)
							{
								goto IL_152;
							}
						}
						else
						{
							string item = contentRestriction.PropValue.Value as string;
							if (!FormsCondition.FormTypeSet.Contains(item))
							{
								throw new RuleParseException(ServerStrings.UnsupportedFormsCondition);
							}
							list3.Add((string)contentRestriction.PropValue.Value);
						}
					}
					else if (propTag3 != PropTag.TransportMessageHeaders)
					{
						if (propTag3 == PropTag.SenderName)
						{
							num4++;
						}
					}
					else
					{
						num10++;
					}
				}
				else if (propTag3 <= PropTag.NormalizedSubject)
				{
					if (propTag3 != PropTag.SenderSearchKey)
					{
						if (propTag3 == PropTag.NormalizedSubject)
						{
							goto IL_152;
						}
					}
					else
					{
						num8++;
					}
				}
				else if (propTag3 != PropTag.Body)
				{
					if (propTag3 != PropTag.DisplayName)
					{
						if (propTag3 == PropTag.SearchKey)
						{
							num9++;
						}
					}
					else
					{
						num5++;
					}
				}
				else
				{
					num6++;
				}
				IL_188:
				k++;
				continue;
				IL_152:
				num7++;
				goto IL_188;
			}
			if (num == list3.Count)
			{
				if (1 == num)
				{
					string value = list3[0];
					if ("IPM.Note.Rules.OofTemplate.Microsoft".Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						if (isException)
						{
							this.Exceptions.Add(MarkedAsOofCondition.Create(this));
							return;
						}
						this.Conditions.Add(MarkedAsOofCondition.Create(this));
						return;
					}
					else if ("IPM.Note.Microsoft.Approval.Request".Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						if (isException)
						{
							this.Exceptions.Add(ApprovalRequestCondition.Create(this));
							return;
						}
						this.Conditions.Add(ApprovalRequestCondition.Create(this));
						return;
					}
				}
				else if (2 == num)
				{
					if ((list3[0].Equals("IPM.Schedule.Meeting.Request", StringComparison.OrdinalIgnoreCase) && list3[1].Equals("IPM.Schedule.Meeting.Canceled", StringComparison.OrdinalIgnoreCase)) || (list3[0].Equals("IPM.Schedule.Meeting.Canceled", StringComparison.OrdinalIgnoreCase) && list3[1].Equals("IPM.Schedule.Meeting.Request", StringComparison.OrdinalIgnoreCase)))
					{
						if (isException)
						{
							this.Exceptions.Add(MeetingMessageCondition.Create(this));
							return;
						}
						this.Conditions.Add(MeetingMessageCondition.Create(this));
						return;
					}
					else if ((list3[0].Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) && list3[1].Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase)) || (list3[0].Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase) && list3[1].Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase)))
					{
						if (isException)
						{
							this.Exceptions.Add(PermissionControlledCondition.Create(this));
							return;
						}
						this.Conditions.Add(PermissionControlledCondition.Create(this));
						return;
					}
				}
				else if (3 == num)
				{
					if (list3.Contains("IPM.Schedule.Meeting.Resp.Pos") && list3.Contains("IPM.Schedule.Meeting.Resp.Neg") && list3.Contains("IPM.Schedule.Meeting.Resp.Tent"))
					{
						if (isException)
						{
							this.Exceptions.Add(MeetingResponseCondition.Create(this));
							return;
						}
						this.Conditions.Add(MeetingResponseCondition.Create(this));
						return;
					}
					else if (list3.Contains("IPM.Note.Secure") && list3.Contains("IPM.Note" + "." + "SMIME.SignedEncrypted") && list3.Contains("IPM.Note" + "." + "SMIME.Encrypted"))
					{
						if (isException)
						{
							this.Exceptions.Add(EncryptedCondition.Create(this));
							return;
						}
						this.Conditions.Add(EncryptedCondition.Create(this));
						return;
					}
				}
				else if (5 == num && list3.Contains("IPM.Note.Microsoft.Voicemail.UM.CA") && list3.Contains("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA") && list3.Contains("IPM.Note.rpmsg.Microsoft.Voicemail.UM") && list3.Contains("IPM.Note.Microsoft.Voicemail.UM") && list3.Contains("IPM.Note.Microsoft.Missed.Voice"))
				{
					if (isException)
					{
						this.Exceptions.Add(VoicemailCondition.Create(this));
						return;
					}
					this.Conditions.Add(VoicemailCondition.Create(this));
					return;
				}
				if (isException)
				{
					this.Exceptions.Add(FormsCondition.Create(ConditionType.FormsCondition, this, list3.ToArray()));
					return;
				}
				this.Conditions.Add(FormsCondition.Create(ConditionType.FormsCondition, this, list3.ToArray()));
				return;
			}
			else
			{
				if (num6 == num || num7 == num || num10 == num)
				{
					for (int l = 0; l < num; l++)
					{
						Restriction.ContentRestriction contentRestriction2 = restrictions[l] as Restriction.ContentRestriction;
						if (contentRestriction2 == null)
						{
							throw new RuleParseException(ServerStrings.UnsupportedContentRestriction);
						}
						object value2 = contentRestriction2.PropValue.Value;
						if (isException)
						{
							if (num6 > 0)
							{
								this.exceptBodyStrings.Add((string)value2);
							}
							else if (num10 > 0)
							{
								this.exceptHeaderStrings.Add((string)value2);
							}
							else
							{
								this.exceptSubjectStrings.Add((string)value2);
							}
						}
						else if (num6 > 0)
						{
							this.containsBodyStrings.Add((string)value2);
						}
						else if (num10 > 0)
						{
							this.containsHeaderStrings.Add((string)value2);
						}
						else
						{
							this.containsSubjectStrings.Add((string)value2);
						}
					}
					return;
				}
				if (num7 == num6 && num7 + num6 == num)
				{
					for (int m = 0; m < num; m += 2)
					{
						Restriction.ContentRestriction contentRestriction3 = restrictions[m] as Restriction.ContentRestriction;
						if (contentRestriction3 == null)
						{
							throw new RuleParseException(ServerStrings.UnsupportedContentRestriction);
						}
						object value3 = contentRestriction3.PropValue.Value;
						if (isException)
						{
							this.exceptSubjectOrBodyStrings.Add((string)value3);
						}
						else
						{
							this.containsSubjectOrBodyStrings.Add((string)value3);
						}
					}
					return;
				}
				if (num4 == num)
				{
					List<Participant> participants2 = new List<Participant>();
					Rule.CrackAddressList(participants2, restrictions);
					this.AddSenderElement(isException, participants2);
					return;
				}
				if (num5 == num)
				{
					List<Participant> participants3 = new List<Participant>();
					Rule.CrackAddressList(participants3, restrictions);
					this.AddRecipientElement(isException, participants3);
					return;
				}
				if (num8 == num || num9 == num)
				{
					for (int n = 0; n < num; n++)
					{
						Restriction.ContentRestriction contentRestriction4 = restrictions[n] as Restriction.ContentRestriction;
						if (contentRestriction4 == null)
						{
							throw new RuleParseException(ServerStrings.UnsupportedContentRestriction);
						}
						string item2 = Rule.ConvertSearchKeyValue((byte[])contentRestriction4.PropValue.Value);
						if (isException)
						{
							if (num9 == num)
							{
								this.exceptRecipientStrings.Add(item2);
							}
							else
							{
								this.exceptSenderStrings.Add(item2);
							}
						}
						else if (num9 == num)
						{
							this.containsRecipientStrings.Add(item2);
						}
						else
						{
							this.containsSenderStrings.Add(item2);
						}
					}
					return;
				}
				throw new RuleParseException(ServerStrings.UnsupportedContentRestriction);
			}
		}

		// Token: 0x06006B50 RID: 27472 RVA: 0x001CA77C File Offset: 0x001C897C
		private void ParseServerRule()
		{
			this.validatingUserInput = false;
			this.Name = this.serverRule.Name;
			this.IsEnabled = ((this.serverRule.StateFlags & RuleStateFlags.Enabled) > (RuleStateFlags)0);
			this.RunOnlyWhileOof = ((this.serverRule.StateFlags & RuleStateFlags.OnlyWhenOOF) > (RuleStateFlags)0);
			this.Sequence = this.serverRule.ExecutionSequence;
			this.isInError = ((this.serverRule.StateFlags & RuleStateFlags.Error) == RuleStateFlags.Error);
			this.provider = this.serverRule.Provider;
			try
			{
				this.ParseServerRuleConditions();
				this.ParseServerRuleActions();
				if ((this.serverRule.StateFlags & RuleStateFlags.ExitAfterExecution) > (RuleStateFlags)0)
				{
					this.Actions.Add(StopProcessingAction.Create(this));
				}
			}
			catch (RuleParseException ex)
			{
				this.isNotSupported = true;
				ExTraceGlobals.StorageTracer.TraceError<string, string>((long)this.GetHashCode(), "ParseServerRule, can't process rule {0}, {1}", this.serverRule.Name, ex.Message);
			}
			this.validatingUserInput = true;
		}

		// Token: 0x06006B51 RID: 27473 RVA: 0x001CA880 File Offset: 0x001C8A80
		private void ParseServerRuleConditions()
		{
			Restriction condition = this.serverRule.Condition;
			this.AddRestrictions(condition, false);
			if (this.toParticipants.Count > 0)
			{
				this.Conditions.Add(SentToRecipientsCondition.Create(this, this.toParticipants.ToArray()));
			}
			if (this.fromParticipants.Count > 0)
			{
				this.Conditions.Add(FromRecipientsCondition.Create(this, this.fromParticipants.ToArray()));
			}
			if (this.containsSubjectStrings.Count > 0)
			{
				this.Conditions.Add(ContainsSubjectStringCondition.Create(this, this.containsSubjectStrings.ToArray()));
			}
			if (this.containsBodyStrings.Count > 0)
			{
				this.Conditions.Add(ContainsBodyStringCondition.Create(this, this.containsBodyStrings.ToArray()));
			}
			if (this.containsSubjectOrBodyStrings.Count > 0)
			{
				this.Conditions.Add(ContainsSubjectOrBodyStringCondition.Create(this, this.containsSubjectOrBodyStrings.ToArray()));
			}
			if (this.containsSenderStrings.Count > 0)
			{
				this.Conditions.Add(ContainsSenderStringCondition.Create(this, this.containsSenderStrings.ToArray()));
			}
			if (this.containsHeaderStrings.Count > 0)
			{
				this.Conditions.Add(ContainsHeaderStringCondition.Create(this, this.containsHeaderStrings.ToArray()));
			}
			if (this.before != null || this.after != null)
			{
				this.Conditions.Add(WithinDateRangeCondition.Create(this, this.after, this.before));
			}
			if (this.lowRange != null || this.highRange != null)
			{
				this.Conditions.Add(WithinSizeRangeCondition.Create(this, this.lowRange, this.highRange));
			}
			if (this.containsRecipientStrings.Count > 0)
			{
				this.Conditions.Add(ContainsRecipientStringCondition.Create(this, this.containsRecipientStrings.ToArray()));
			}
			if (this.assignedCategoriesStrings.Count > 0)
			{
				this.Conditions.Add(AssignedCategoriesCondition.Create(this, this.assignedCategoriesStrings.ToArray()));
			}
			if (this.isMessageClassificationCondition)
			{
				this.Conditions.Add(MessageClassificationCondition.Create(this, this.messageClassificationStrings.ToArray()));
			}
			if (this.exceptToParticipants.Count > 0)
			{
				this.Exceptions.Add(SentToRecipientsCondition.Create(this, this.exceptToParticipants.ToArray()));
			}
			if (this.exceptFromParticipants.Count > 0)
			{
				this.Exceptions.Add(FromRecipientsCondition.Create(this, this.exceptFromParticipants.ToArray()));
			}
			if (this.exceptSubjectStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsSubjectStringCondition.Create(this, this.exceptSubjectStrings.ToArray()));
			}
			if (this.exceptBodyStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsBodyStringCondition.Create(this, this.exceptBodyStrings.ToArray()));
			}
			if (this.exceptSubjectOrBodyStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsSubjectOrBodyStringCondition.Create(this, this.exceptSubjectOrBodyStrings.ToArray()));
			}
			if (this.exceptSenderStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsSenderStringCondition.Create(this, this.exceptSenderStrings.ToArray()));
			}
			if (this.exceptHeaderStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsHeaderStringCondition.Create(this, this.exceptHeaderStrings.ToArray()));
			}
			if (this.exceptBefore != null || this.exceptAfter != null)
			{
				this.Exceptions.Add(WithinDateRangeCondition.Create(this, this.exceptAfter, this.exceptBefore));
			}
			if (this.exceptLowRange != null || this.exceptHighRange != null)
			{
				this.Exceptions.Add(WithinSizeRangeCondition.Create(this, this.exceptLowRange, this.exceptHighRange));
			}
			if (this.exceptRecipientStrings.Count > 0)
			{
				this.Exceptions.Add(ContainsRecipientStringCondition.Create(this, this.exceptRecipientStrings.ToArray()));
			}
			if (this.exceptCategoriesStrings.Count > 0)
			{
				this.Exceptions.Add(AssignedCategoriesCondition.Create(this, this.exceptCategoriesStrings.ToArray()));
			}
			if (this.isMessageClassificationException)
			{
				this.Exceptions.Add(MessageClassificationCondition.Create(this, this.exceptMessageClassificationStrings.ToArray()));
			}
		}

		// Token: 0x06006B52 RID: 27474 RVA: 0x001CACBC File Offset: 0x001C8EBC
		private bool AddRestrictions(Restriction res, bool isException)
		{
			if (res is Restriction.AndRestriction)
			{
				Restriction.AndRestriction andRestriction = res as Restriction.AndRestriction;
				if (andRestriction.Restrictions.Length == 3 && andRestriction.Restrictions[2] is Restriction.PropertyRestriction && (andRestriction.Restrictions[2] as Restriction.PropertyRestriction).PropTag == PropTag.DisplayCc)
				{
					if (isException)
					{
						this.Exceptions.Add(SentOnlyToMeCondition.Create(this));
					}
					else
					{
						this.Conditions.Add(SentOnlyToMeCondition.Create(this));
					}
				}
				else if (andRestriction.Restrictions[0] is Restriction.PropertyRestriction && (andRestriction.Restrictions[0] as Restriction.PropertyRestriction).PropTag == PropTag.MessageCcMe)
				{
					if (isException)
					{
						this.Exceptions.Add(SentCcMeCondition.Create(this));
					}
					else
					{
						this.Conditions.Add(SentCcMeCondition.Create(this));
					}
				}
				else if (this.IsNdrRestrictionSet(andRestriction))
				{
					if (isException)
					{
						this.Exceptions.Add(NdrCondition.Create(this));
					}
					else
					{
						this.Conditions.Add(NdrCondition.Create(this));
					}
				}
				else if (this.IsFlaggedRestrictionSet(andRestriction))
				{
					Restriction.PropertyRestriction propertyRestriction = andRestriction.Restrictions[1] as Restriction.PropertyRestriction;
					string @string = propertyRestriction.PropValue.GetString();
					if (isException)
					{
						this.Exceptions.Add(FlaggedForActionCondition.Create(this, @string));
					}
					else
					{
						this.Conditions.Add(FlaggedForActionCondition.Create(this, @string));
					}
				}
				else
				{
					for (int i = 0; i < andRestriction.Restrictions.Length; i++)
					{
						this.AddRestrictions(andRestriction.Restrictions[i], isException);
					}
				}
			}
			else if (res is Restriction.OrRestriction)
			{
				this.ParseOrRestriction(res as Restriction.OrRestriction, isException);
			}
			else if (res is Restriction.NotRestriction)
			{
				Restriction.NotRestriction notRestriction = res as Restriction.NotRestriction;
				this.AddRestrictions(notRestriction.Restriction, !isException);
			}
			else if (res is Restriction.ContentRestriction)
			{
				Restriction.ContentRestriction contentRestriction = res as Restriction.ContentRestriction;
				PropTag propTag = contentRestriction.PropTag;
				if (propTag == PropTag.SenderName)
				{
					List<Participant> participants = new List<Participant>();
					Rule.CrackAddressList(participants, new Restriction[]
					{
						res
					});
					this.AddSenderElement(isException, participants);
				}
				else if (propTag == PropTag.DisplayName)
				{
					List<Participant> participants2 = new List<Participant>();
					Rule.CrackAddressList(participants2, new Restriction[]
					{
						res
					});
					this.AddRecipientElement(isException, participants2);
				}
				else
				{
					if (contentRestriction.MultiValued)
					{
						propTag &= (PropTag)4096U;
					}
					this.AddElementFromProp(propTag, contentRestriction.PropValue.Value, (int)contentRestriction.Flags, isException);
				}
			}
			else if (res is Restriction.PropertyRestriction)
			{
				Restriction.PropertyRestriction propertyRestriction2 = res as Restriction.PropertyRestriction;
				PropTag propTag2 = propertyRestriction2.MultiValued ? (propertyRestriction2.PropTag | (PropTag)4096U) : propertyRestriction2.PropTag;
				this.AddElementFromProp(propTag2, propertyRestriction2.PropValue.Value, (int)propertyRestriction2.Op, isException);
			}
			else if (res is Restriction.CommentRestriction)
			{
				Restriction.CommentRestriction commentRestriction = res as Restriction.CommentRestriction;
				Rule.InboxSpecialComment inboxSpecialComment = (Rule.InboxSpecialComment)commentRestriction.Values[0].Value;
				if (inboxSpecialComment == Rule.InboxSpecialComment.Resolved)
				{
					List<Participant> participants3 = new List<Participant>();
					Rule.CrackAddressList(participants3, new Restriction[]
					{
						res
					});
					try
					{
						if (Rule.IsSenderRst(res))
						{
							this.AddSenderElement(isException, participants3);
						}
						else
						{
							this.AddRecipientElement(isException, participants3);
						}
						goto IL_52C;
					}
					catch (ArgumentException)
					{
						throw new RuleParseException(ServerStrings.MalformedCommentRestriction);
					}
				}
				this.AddRestrictions(commentRestriction.Restriction, isException);
			}
			else if (res is Restriction.BitMaskRestriction)
			{
				Restriction.BitMaskRestriction bitMaskRestriction = res as Restriction.BitMaskRestriction;
				this.AddElementFromBitmaskProp(bitMaskRestriction.Tag, bitMaskRestriction.Mask, bitMaskRestriction.Bmr, isException);
			}
			else
			{
				if (res is Restriction.SubRestriction)
				{
					Restriction restriction = (res as Restriction.SubRestriction).Restriction;
					if (restriction is Restriction.OrRestriction && (restriction as Restriction.OrRestriction).Restrictions[0] is Restriction.OrRestriction && ((restriction as Restriction.OrRestriction).Restrictions[0] as Restriction.OrRestriction).Restrictions[0] is Restriction.AndRestriction && (((restriction as Restriction.OrRestriction).Restrictions[0] as Restriction.OrRestriction).Restrictions[0] as Restriction.AndRestriction).Restrictions[0] is Restriction.ContentRestriction)
					{
						Restriction[] restrictions = (restriction as Restriction.OrRestriction).Restrictions;
						List<Participant> list = new List<Participant>();
						for (int j = 0; j < restrictions.Length; j++)
						{
							Restriction.ContentRestriction contentRestriction2 = ((restrictions[j] as Restriction.OrRestriction).Restrictions[0] as Restriction.AndRestriction).Restrictions[0] as Restriction.ContentRestriction;
							object value = contentRestriction2.PropValue.Value;
							PropTag propTag3 = contentRestriction2.PropTag;
							list.Add(new Participant((string)value, null, null));
						}
						this.AddRecipientElement(isException, list);
						goto IL_52C;
					}
					if (!(restriction is Restriction.ContentRestriction))
					{
						try
						{
							this.AddRestrictions(restriction, isException);
							goto IL_52C;
						}
						finally
						{
							this.hack = true;
						}
					}
					try
					{
						this.AddRestrictions(restriction, isException);
						goto IL_52C;
					}
					finally
					{
						this.hack = true;
					}
				}
				if (res is Restriction.ExistRestriction)
				{
					Restriction.ExistRestriction existRestriction = res as Restriction.ExistRestriction;
					if (PropTag.MessageClass != existRestriction.Tag && PropTag.Sensitivity != existRestriction.Tag && (PropTag)1071841291U != existRestriction.Tag)
					{
						throw new RuleParseException(ServerStrings.UnsupportedExistRestriction);
					}
				}
			}
			IL_52C:
			return this.hack;
		}

		// Token: 0x06006B53 RID: 27475 RVA: 0x001CB224 File Offset: 0x001C9424
		private void ParseServerRuleActions()
		{
			this.AddActions(this.serverRule.Actions);
		}

		// Token: 0x06006B54 RID: 27476 RVA: 0x001CB238 File Offset: 0x001C9438
		private void AddActions(RuleAction[] actions)
		{
			if (actions == null || actions.Length == 0)
			{
				return;
			}
			for (int i = 0; i < actions.Length; i++)
			{
				if (actions[i] is RuleAction.InMailboxMove)
				{
					RuleAction.InMailboxMove inMailboxMove = actions[i] as RuleAction.InMailboxMove;
					MailboxSession mailboxSession = this.Folder.Session as MailboxSession;
					if (ArrayComparer<byte>.Comparer.Equals(inMailboxMove.FolderEntryID, mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems).ProviderLevelItemId))
					{
						this.Actions.Add(DeleteAction.Create(this));
					}
					else
					{
						StoreObjectId folderId = StoreObjectId.FromProviderSpecificId(inMailboxMove.FolderEntryID, StoreObjectType.Folder);
						this.Actions.Add(MoveToFolderAction.Create(folderId, this));
					}
				}
				else if (actions[i] is RuleAction.InMailboxCopy)
				{
					RuleAction.InMailboxCopy inMailboxCopy = actions[i] as RuleAction.InMailboxCopy;
					StoreObjectId folderId2 = StoreObjectId.FromProviderSpecificId(inMailboxCopy.FolderEntryID, StoreObjectType.Folder);
					this.Actions.Add(CopyToFolderAction.Create(folderId2, this));
				}
				else
				{
					if (actions[i] is RuleAction.ExternalMove)
					{
						throw new RuleParseException(ServerStrings.UnsupportedAction);
					}
					if (actions[i] is RuleAction.ExternalCopy)
					{
						throw new RuleParseException(ServerStrings.UnsupportedAction);
					}
					if (actions[i] is RuleAction.Reply)
					{
						RuleAction.Reply reply = actions[i] as RuleAction.Reply;
						StoreObjectId messageId = null;
						Guid replyTemplateGuid = reply.ReplyTemplateGuid;
						try
						{
							messageId = StoreObjectId.FromProviderSpecificId(reply.ReplyTemplateMessageEntryID);
							using (MessageItem messageItem = MessageItem.Bind(this.Folder.Session, messageId, new PropertyDefinition[]
							{
								ItemSchema.Subject
							}))
							{
								string subject = messageItem.Subject;
							}
						}
						catch (ObjectNotFoundException e)
						{
							throw new RuleParseException(ServerStrings.NoTemplateMessage, e);
						}
						this.Actions.Add(ServerReplyMessageAction.Create(messageId, replyTemplateGuid, this));
					}
					else
					{
						if (actions[i] is RuleAction.Defer)
						{
							throw new RuleParseException(ServerStrings.NoDeferredActions);
						}
						if (actions[i] is RuleAction.Forward)
						{
							RuleAction.Forward forward = actions[i] as RuleAction.Forward;
							List<Participant> list = new List<Participant>();
							for (int j = 0; j < forward.Recipients.Length; j++)
							{
								try
								{
									list.Add(Rule.ParticipantFromAdrEntry(forward.Recipients[j]));
								}
								catch (ArgumentException)
								{
									throw new RuleParseException(ServerStrings.MalformedAdrEntry);
								}
							}
							if ((forward.Flags & RuleAction.Forward.ActionFlags.DoNotMungeMessage) > RuleAction.Forward.ActionFlags.None && (forward.Flags & RuleAction.Forward.ActionFlags.PreserveSender) > RuleAction.Forward.ActionFlags.None)
							{
								this.Actions.Add(RedirectToRecipientsAction.Create(list, this));
							}
							else if ((forward.Flags & RuleAction.Forward.ActionFlags.ForwardAsAttachment) > RuleAction.Forward.ActionFlags.None)
							{
								this.Actions.Add(ForwardAsAttachmentToRecipientsAction.Create(list, this));
							}
							else if ((forward.Flags & RuleAction.Forward.ActionFlags.SendSmsAlert) > RuleAction.Forward.ActionFlags.None)
							{
								this.Actions.Add(SendSmsAlertToRecipientsAction.Create(list, this));
							}
							else
							{
								this.Actions.Add(ForwardToRecipientsAction.Create(list, this));
							}
						}
						else if (actions[i] is RuleAction.Delegate)
						{
							RuleAction.Delegate @delegate = actions[i] as RuleAction.Delegate;
							List<Participant> list2 = new List<Participant>();
							if (@delegate.Recipients != null)
							{
								for (int k = 0; k < @delegate.Recipients.Length; k++)
								{
									try
									{
										list2.Add(Rule.ParticipantFromAdrEntry(@delegate.Recipients[k]));
									}
									catch (ArgumentException)
									{
										throw new RuleParseException(ServerStrings.MalformedAdrEntry);
									}
								}
							}
							this.Actions.Add(RedirectToRecipientsAction.Create(list2, this));
						}
						else if (actions[i] is RuleAction.Tag)
						{
							RuleAction.Tag tag = actions[i] as RuleAction.Tag;
							if (tag.Value.PropTag == PropTag.Importance)
							{
								this.Actions.Add(MarkImportanceAction.Create((Importance)tag.Value.Value, this));
							}
							else if (tag.Value.PropTag == PropTag.Sensitivity)
							{
								this.Actions.Add(MarkSensitivityAction.Create((Sensitivity)tag.Value.Value, this));
							}
							else if (tag.Value.PropTag == (PropTag)277872643U)
							{
								this.Actions.Add(FlagMessageAction.Create((FlagStatus)tag.Value.Value, this));
							}
							else
							{
								GuidNamePropertyDefinition guidNamePropertyDefinition = this.PropTagToPropertyDefinitionFromCache(tag.Value.PropTag) as GuidNamePropertyDefinition;
								if (guidNamePropertyDefinition == null || !guidNamePropertyDefinition.Equals(Rule.NamedDefinitions[0]))
								{
									throw new RuleParseException(ServerStrings.UnsupportedAction);
								}
								this.Actions.Add(AssignCategoriesAction.Create((string[])tag.Value.Value, this));
							}
						}
						else if (actions[i] is RuleAction.Delete)
						{
							this.Actions.Add(PermanentDeleteAction.Create(this));
						}
						else
						{
							if (!(actions[i] is RuleAction.MarkAsRead))
							{
								throw new RuleParseException(ServerStrings.UnsupportedAction);
							}
							this.Actions.Add(MarkAsReadAction.Create(this));
						}
					}
				}
			}
		}

		// Token: 0x06006B55 RID: 27477 RVA: 0x001CB730 File Offset: 0x001C9930
		internal static Participant ParticipantFromAdrEntry(AdrEntry adrEntry)
		{
			string displayName = string.Empty;
			byte[] entryIdBytes = null;
			byte[] searchKey = null;
			for (int i = 0; i < adrEntry.Values.Length; i++)
			{
				PropTag propTag = adrEntry.Values[i].PropTag;
				if (propTag != PropTag.EntryId)
				{
					if (propTag != PropTag.DisplayName)
					{
						if (propTag == PropTag.SearchKey)
						{
							searchKey = (byte[])adrEntry.Values[i].Value;
						}
					}
					else
					{
						displayName = (string)adrEntry.Values[i].Value;
					}
				}
				else
				{
					entryIdBytes = (byte[])adrEntry.Values[i].Value;
				}
			}
			return Rule.CreateParticipant(displayName, entryIdBytes, searchKey);
		}

		// Token: 0x06006B56 RID: 27478 RVA: 0x001CB7E0 File Offset: 0x001C99E0
		internal static AdrEntry AdrEntryFromParticipant(Participant participant)
		{
			string displayName = participant.DisplayName;
			string text = participant.RoutingType;
			string emailAddress = participant.EmailAddress;
			byte[] value = Rule.SearchKeyFromParticipant(participant);
			LegacyRecipientDisplayType legacyRecipientDisplayType = LegacyRecipientDisplayType.MailUser;
			byte[] array = null;
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.RecipientTableSecondary);
			if (participantEntryId != null)
			{
				legacyRecipientDisplayType = participant.GetValueOrDefault<LegacyRecipientDisplayType>(ParticipantSchema.DisplayType);
				array = participantEntryId.ToByteArray();
				if (participantEntryId is StoreParticipantEntryId && ((StoreParticipantEntryId)participantEntryId).IsDL == true)
				{
					text = "MAPIPDL";
				}
			}
			List<PropValue> list = new List<PropValue>();
			if (array != null)
			{
				list.Add(new PropValue(PropTag.EntryId, array));
			}
			list.Add(new PropValue(PropTag.DisplayName, displayName));
			list.Add(new PropValue(PropTag.DisplayType, (int)legacyRecipientDisplayType));
			if (string.IsNullOrEmpty(text) || text == "SMTP")
			{
				list.Add(new PropValue(PropTag.SmtpAddress, emailAddress));
			}
			list.Add(new PropValue(PropTag.SearchKey, value));
			list.Add(new PropValue(PropTag.RecipientType, RecipientType.To));
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(new PropValue(PropTag.AddrType, text));
			}
			else
			{
				list.Add(new PropValue(PropTag.AddrType, "SMTP"));
			}
			list.Add(new PropValue(PropTag.EmailAddress, emailAddress));
			return new AdrEntry(list.ToArray());
		}

		// Token: 0x06006B57 RID: 27479 RVA: 0x001CB950 File Offset: 0x001C9B50
		internal static Restriction OrAddressList(IList<Participant> participants, PropTag tagDisplayName)
		{
			if (participants.Count == 1)
			{
				return Rule.ContentOrPropertyFromParticipant(participants[0], tagDisplayName);
			}
			Restriction[] array = new Restriction[participants.Count];
			bool flag = false;
			for (int i = 0; i < participants.Count; i++)
			{
				array[i] = Rule.ContentOrPropertyFromParticipant(participants[i], tagDisplayName);
				if (array[i] is Restriction.ContentRestriction)
				{
					flag = true;
				}
			}
			if (flag)
			{
				Array.Sort(array, Rule.addressListComparer);
			}
			return Restriction.Or(array);
		}

		// Token: 0x06006B58 RID: 27480 RVA: 0x001CB9C8 File Offset: 0x001C9BC8
		internal static Rule CheckRuleParameter(object[] parameters)
		{
			if (parameters.Length == 0)
			{
				throw new ArgumentException("rule");
			}
			if (parameters[0] == null)
			{
				throw new ArgumentNullException("rule");
			}
			Rule rule = parameters[0] as Rule;
			if (rule == null)
			{
				throw new ArgumentException("rule");
			}
			return rule;
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x06006B59 RID: 27481 RVA: 0x001CBA0D File Offset: 0x001C9C0D
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x06006B5A RID: 27482 RVA: 0x001CBA15 File Offset: 0x001C9C15
		internal Rule ServerRule
		{
			get
			{
				return this.serverRule;
			}
		}

		// Token: 0x06006B5B RID: 27483 RVA: 0x001CBA1D File Offset: 0x001C9C1D
		internal void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x06006B5C RID: 27484 RVA: 0x001CBA26 File Offset: 0x001C9C26
		internal void ClearDirty()
		{
			this.isDirty = false;
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x001CBA2F File Offset: 0x001C9C2F
		internal bool ValidatingUserInput
		{
			get
			{
				return this.validatingUserInput;
			}
		}

		// Token: 0x06006B5E RID: 27486 RVA: 0x001CBA38 File Offset: 0x001C9C38
		internal PropTag PropertyDefinitionToPropTagFromCache(NamedPropertyDefinition propertyDefinition)
		{
			NamedPropMap mapping = NamedPropMapCache.Default.GetMapping(this.folder.Session);
			NamedPropertyDefinition.NamedPropertyKey key = propertyDefinition.GetKey();
			NamedProp namedProp = key.NamedProp;
			ushort num;
			if (mapping == null || !mapping.TryGetPropIdFromNamedProp(namedProp, out num))
			{
				PropertyTagCache.Cache.PropTagsFromPropertyDefinitions(this.folder.MapiFolder, this.folder.Session, Rule.NamedDefinitions);
			}
			PropTag propTag = PropertyTagCache.Cache.PropTagFromPropertyDefinition(this.folder.MapiFolder, this.folder.Session, propertyDefinition);
			if (PropTag.Unresolved == propTag)
			{
				throw new RuleParseException(ServerStrings.MapiCannotGetIDFromNames);
			}
			return propTag;
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x001CBAD8 File Offset: 0x001C9CD8
		internal void ThrowValidateException(Rule.ThrowExceptionDelegate thrower, string errStr)
		{
			if (this.validatingUserInput)
			{
				thrower();
				return;
			}
			throw new RuleParseException(errStr);
		}

		// Token: 0x04003D1A RID: 15642
		private const string Delimiter = ";";

		// Token: 0x04003D1B RID: 15643
		internal const string OL97Provider = "MSFT:TDX Rules";

		// Token: 0x04003D1C RID: 15644
		internal const string OL98PlusProvider = "RuleOrganizer";

		// Token: 0x04003D1D RID: 15645
		internal const string Exchange14Provider = "ExchangeMailboxRules14";

		// Token: 0x04003D1E RID: 15646
		internal const string OLKRuleMsgClass = "IPM.RuleOrganizer";

		// Token: 0x04003D1F RID: 15647
		internal const string NetFolders = "#NET FOLDERS#";

		// Token: 0x04003D20 RID: 15648
		internal const string IPMRulesOofTemplate = "IPM.Note.Rules.OofTemplate.Microsoft";

		// Token: 0x04003D21 RID: 15649
		internal const string IPMScheduleMeetingRequest = "IPM.Schedule.Meeting.Request";

		// Token: 0x04003D22 RID: 15650
		internal const string IPMScheduleMeetingCanceled = "IPM.Schedule.Meeting.Canceled";

		// Token: 0x04003D23 RID: 15651
		internal const PropTag ComCritCommentIdPropTag = (PropTag)1610612739U;

		// Token: 0x04003D24 RID: 15652
		internal const PropTag DisplayTypePropTag = PropTag.DisplayType;

		// Token: 0x04003D25 RID: 15653
		public static readonly string[] ProviderStringArray = new string[]
		{
			null,
			"MSFT:TDX Rules",
			"RuleOrganizer",
			"ExchangeMailboxRules14"
		};

		// Token: 0x04003D26 RID: 15654
		internal static readonly NamedPropertyDefinition[] NamedDefinitions = new NamedPropertyDefinition[]
		{
			InternalSchema.Categories,
			InternalSchema.IsClassified,
			InternalSchema.ClassificationGuid,
			InternalSchema.IsReadReceipt,
			InternalSchema.IsSigned,
			InternalSchema.FlagRequest
		};

		// Token: 0x04003D27 RID: 15655
		private bool hack;

		// Token: 0x04003D28 RID: 15656
		private Rule serverRule;

		// Token: 0x04003D29 RID: 15657
		private Folder folder;

		// Token: 0x04003D2A RID: 15658
		private string name;

		// Token: 0x04003D2B RID: 15659
		private RuleId ruleId;

		// Token: 0x04003D2C RID: 15660
		private bool isEnabled;

		// Token: 0x04003D2D RID: 15661
		private bool onlyOof;

		// Token: 0x04003D2E RID: 15662
		private bool isInError;

		// Token: 0x04003D2F RID: 15663
		private bool isNotSupported;

		// Token: 0x04003D30 RID: 15664
		private bool isDirty;

		// Token: 0x04003D31 RID: 15665
		private int sequence;

		// Token: 0x04003D32 RID: 15666
		private string provider;

		// Token: 0x04003D33 RID: 15667
		private Rule.ProviderIdEnum providerId;

		// Token: 0x04003D34 RID: 15668
		private ActionList actions;

		// Token: 0x04003D35 RID: 15669
		private ConditionList conditions;

		// Token: 0x04003D36 RID: 15670
		private ConditionList exceptions;

		// Token: 0x04003D37 RID: 15671
		private List<Participant> toParticipants;

		// Token: 0x04003D38 RID: 15672
		private List<Participant> fromParticipants;

		// Token: 0x04003D39 RID: 15673
		private List<string> containsSubjectStrings;

		// Token: 0x04003D3A RID: 15674
		private List<string> containsBodyStrings;

		// Token: 0x04003D3B RID: 15675
		private List<string> containsSubjectOrBodyStrings;

		// Token: 0x04003D3C RID: 15676
		private List<string> containsSenderStrings;

		// Token: 0x04003D3D RID: 15677
		private List<string> containsHeaderStrings;

		// Token: 0x04003D3E RID: 15678
		private List<string> containsRecipientStrings;

		// Token: 0x04003D3F RID: 15679
		private List<string> assignedCategoriesStrings;

		// Token: 0x04003D40 RID: 15680
		private List<string> messageClassificationStrings;

		// Token: 0x04003D41 RID: 15681
		private List<Participant> exceptToParticipants;

		// Token: 0x04003D42 RID: 15682
		private List<Participant> exceptFromParticipants;

		// Token: 0x04003D43 RID: 15683
		private List<string> exceptSubjectStrings;

		// Token: 0x04003D44 RID: 15684
		private List<string> exceptBodyStrings;

		// Token: 0x04003D45 RID: 15685
		private List<string> exceptSubjectOrBodyStrings;

		// Token: 0x04003D46 RID: 15686
		private List<string> exceptSenderStrings;

		// Token: 0x04003D47 RID: 15687
		private List<string> exceptRecipientStrings;

		// Token: 0x04003D48 RID: 15688
		private List<string> exceptHeaderStrings;

		// Token: 0x04003D49 RID: 15689
		private List<string> exceptCategoriesStrings;

		// Token: 0x04003D4A RID: 15690
		private List<string> exceptMessageClassificationStrings;

		// Token: 0x04003D4B RID: 15691
		private bool validatingUserInput = true;

		// Token: 0x04003D4C RID: 15692
		private bool isNew = true;

		// Token: 0x04003D4D RID: 15693
		private bool isMessageClassificationCondition;

		// Token: 0x04003D4E RID: 15694
		private bool isMessageClassificationException;

		// Token: 0x04003D4F RID: 15695
		private int? lowRange = null;

		// Token: 0x04003D50 RID: 15696
		private int? highRange = null;

		// Token: 0x04003D51 RID: 15697
		private int? exceptLowRange = null;

		// Token: 0x04003D52 RID: 15698
		private int? exceptHighRange = null;

		// Token: 0x04003D53 RID: 15699
		private ExDateTime? before = null;

		// Token: 0x04003D54 RID: 15700
		private ExDateTime? after = null;

		// Token: 0x04003D55 RID: 15701
		private ExDateTime? exceptBefore = null;

		// Token: 0x04003D56 RID: 15702
		private ExDateTime? exceptAfter = null;

		// Token: 0x04003D57 RID: 15703
		private static Rule.AddressListRestrictionComparer addressListComparer = new Rule.AddressListRestrictionComparer();

		// Token: 0x02000BB9 RID: 3001
		internal enum NamedDefinitionIndex
		{
			// Token: 0x04003D59 RID: 15705
			Categories,
			// Token: 0x04003D5A RID: 15706
			IsClassified,
			// Token: 0x04003D5B RID: 15707
			ClassificationGuid,
			// Token: 0x04003D5C RID: 15708
			IsReadReceipt,
			// Token: 0x04003D5D RID: 15709
			IsSigned,
			// Token: 0x04003D5E RID: 15710
			FlagRequest
		}

		// Token: 0x02000BBA RID: 3002
		// (Invoke) Token: 0x06006B62 RID: 27490
		internal delegate void ThrowExceptionDelegate();

		// Token: 0x02000BBB RID: 3003
		private class AddressListRestrictionComparer : IComparer
		{
			// Token: 0x06006B65 RID: 27493 RVA: 0x001CBB6C File Offset: 0x001C9D6C
			public int Compare(object x, object y)
			{
				Restriction restriction = x as Restriction;
				Restriction restriction2 = y as Restriction;
				if (restriction == null || restriction2 == null)
				{
					return 0;
				}
				if (restriction.GetType() == restriction2.GetType())
				{
					return 0;
				}
				if (restriction is Restriction.CommentRestriction)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x02000BBC RID: 3004
		public enum ProviderIdEnum
		{
			// Token: 0x04003D60 RID: 15712
			Unknown,
			// Token: 0x04003D61 RID: 15713
			OL97,
			// Token: 0x04003D62 RID: 15714
			OL98Plus,
			// Token: 0x04003D63 RID: 15715
			Exchange14
		}

		// Token: 0x02000BBD RID: 3005
		internal enum InboxSpecialComment
		{
			// Token: 0x04003D65 RID: 15717
			Resolved = 1,
			// Token: 0x04003D66 RID: 15718
			TopCmt,
			// Token: 0x04003D67 RID: 15719
			Forms,
			// Token: 0x04003D68 RID: 15720
			FormProps
		}

		// Token: 0x02000BBE RID: 3006
		internal struct ProviderData
		{
			// Token: 0x06006B67 RID: 27495 RVA: 0x001CBBB8 File Offset: 0x001C9DB8
			public ProviderData(ArraySegment<byte> buffer)
			{
				this.Version = BitConverter.ToUInt32(buffer.Array, buffer.Offset + Rule.ProviderData.VersionOffset);
				this.RuleSearchKey = BitConverter.ToUInt32(buffer.Array, buffer.Offset + Rule.ProviderData.RuleSearchKeyOffset);
				this.TimeStamp = BitConverter.ToInt64(buffer.Array, buffer.Offset + Rule.ProviderData.TimeStampOffset);
			}

			// Token: 0x06006B68 RID: 27496 RVA: 0x001CBC24 File Offset: 0x001C9E24
			public byte[] ToByteArray()
			{
				byte[] array = new byte[Rule.ProviderData.Size];
				ExBitConverter.Write(this.Version, array, Rule.ProviderData.VersionOffset);
				ExBitConverter.Write(this.RuleSearchKey, array, Rule.ProviderData.RuleSearchKeyOffset);
				ExBitConverter.Write(this.TimeStamp, array, Rule.ProviderData.TimeStampOffset);
				return array;
			}

			// Token: 0x04003D69 RID: 15721
			public uint Version;

			// Token: 0x04003D6A RID: 15722
			public uint RuleSearchKey;

			// Token: 0x04003D6B RID: 15723
			public long TimeStamp;

			// Token: 0x04003D6C RID: 15724
			private static readonly int VersionOffset = (int)Marshal.OffsetOf(typeof(Rule.ProviderData), "Version");

			// Token: 0x04003D6D RID: 15725
			private static readonly int RuleSearchKeyOffset = (int)Marshal.OffsetOf(typeof(Rule.ProviderData), "RuleSearchKey");

			// Token: 0x04003D6E RID: 15726
			private static readonly int TimeStampOffset = (int)Marshal.OffsetOf(typeof(Rule.ProviderData), "TimeStamp");

			// Token: 0x04003D6F RID: 15727
			private static readonly int Size = Marshal.SizeOf(typeof(Rule.ProviderData));
		}
	}
}
