using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClutterOverrideManager : IClutterOverrideManager, ICollection<SmtpAddress>, IEnumerable<SmtpAddress>, IEnumerable, IDisposable
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0002B180 File Offset: 0x00029380
		internal ClutterOverrideManager(StoreSession session)
		{
			this.session = session;
			this.Load();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002B195 File Offset: 0x00029395
		public int Count
		{
			get
			{
				return this.neverClutterSenders.Count;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0002B1A2 File Offset: 0x000293A2
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002B1BC File Offset: 0x000293BC
		public void Add(SmtpAddress smtpAddress)
		{
			ClutterUtilities.ValidateSmtpAddress(smtpAddress);
			int num = this.neverClutterSenders.FindIndex((SmtpAddress sender) => sender.Equals(smtpAddress));
			if (num >= 0)
			{
				this.neverClutterSenders.RemoveAt(num);
			}
			this.neverClutterSenders.Add(smtpAddress);
			this.isDirty = true;
			if (this.neverClutterSenders.Count > 1024)
			{
				this.neverClutterSenders.RemoveAt(0);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002B258 File Offset: 0x00029458
		public bool Remove(SmtpAddress smtpAddress)
		{
			ClutterUtilities.ValidateSmtpAddress(smtpAddress);
			int num = this.neverClutterSenders.FindIndex((SmtpAddress sender) => sender.Equals(smtpAddress));
			if (num >= 0)
			{
				this.neverClutterSenders.RemoveAt(num);
				this.isDirty = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002B2AF File Offset: 0x000294AF
		public bool Contains(SmtpAddress smtpAddress)
		{
			ClutterUtilities.ValidateSmtpAddress(smtpAddress);
			return this.neverClutterSenders.Contains(smtpAddress);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002B2C3 File Offset: 0x000294C3
		public void Clear()
		{
			if (this.neverClutterSenders.Any<SmtpAddress>())
			{
				this.neverClutterSenders.Clear();
				this.isDirty = true;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002B2E4 File Offset: 0x000294E4
		public void CopyTo(SmtpAddress[] array, int arrayIndex)
		{
			this.neverClutterSenders.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002B2F3 File Offset: 0x000294F3
		public IEnumerator<SmtpAddress> GetEnumerator()
		{
			return this.neverClutterSenders.GetEnumerator();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0002B305 File Offset: 0x00029505
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002B30D File Offset: 0x0002950D
		public void Dispose()
		{
			if (this.neverClutterSenders != null)
			{
				this.neverClutterSenders.Clear();
				this.neverClutterSenders = null;
			}
			this.neverClutterRule = null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002B330 File Offset: 0x00029530
		public void Save()
		{
			if (this.isDirty)
			{
				this.neverClutterRule = ClutterOverrideManager.PrepareRule(this.neverClutterRule, this.neverClutterSenders, this.markNeverClutterTag);
				this.neverClutterRule = ClutterOverrideManager.SaveNeverClutterRule(this, this.session, this.neverClutterRule);
				this.isDirty = false;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002B384 File Offset: 0x00029584
		internal static Rule PrepareRule(Rule rule, IEnumerable<SmtpAddress> neverClutterSenders, PropTag neverClutterTag)
		{
			if (rule == null)
			{
				rule = new Rule();
				if (neverClutterSenders.Any<SmtpAddress>())
				{
					rule.Operation = RuleOperation.Create;
				}
				else
				{
					rule.Operation = RuleOperation.NoOp;
				}
			}
			else if (neverClutterSenders.Any<SmtpAddress>())
			{
				rule.Operation = RuleOperation.Update;
			}
			else
			{
				rule.Operation = RuleOperation.Delete;
			}
			rule.ExecutionSequence = 0;
			rule.Level = 0;
			rule.StateFlags = RuleStateFlags.Enabled;
			rule.UserFlags = 0U;
			rule.ExtraProperties = null;
			rule.IsExtended = true;
			rule.Name = "Never Clutter Rule";
			rule.Provider = "NeverClutterOverrideRule";
			Rule.ProviderData providerData;
			providerData.Version = 1U;
			providerData.RuleSearchKey = 0U;
			providerData.TimeStamp = ExDateTime.UtcNow.ToFileTimeUtc();
			rule.ProviderData = providerData.ToByteArray();
			rule.Actions = ClutterOverrideManager.BuildRuleActions(neverClutterTag);
			rule.Condition = ClutterOverrideManager.BuildRuleCondition(neverClutterSenders);
			return rule;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002B458 File Offset: 0x00029658
		internal static RuleAction[] BuildRuleActions(PropTag neverClutterTag)
		{
			return new RuleAction[]
			{
				new RuleAction.Tag(new PropValue(neverClutterTag, true))
			};
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002B489 File Offset: 0x00029689
		internal static Restriction BuildRuleCondition(IEnumerable<SmtpAddress> neverClutterSenders)
		{
			return Restriction.Or((from address in neverClutterSenders
			select ClutterOverrideManager.BuildSenderCondition(address)).ToArray<Restriction>());
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002B4B8 File Offset: 0x000296B8
		internal static Restriction BuildSenderCondition(SmtpAddress smtpAddress)
		{
			return Restriction.Content(PropTag.SenderSmtpAddress, smtpAddress.ToString(), ContentFlags.IgnoreCase);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002B4D8 File Offset: 0x000296D8
		internal static Rule SaveNeverClutterRule(object mapiThis, StoreSession session, Rule rule)
		{
			Rule result;
			using (Folder folder = Folder.Bind(session, session.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				bool flag = false;
				try
				{
					if (session != null)
					{
						session.BeginMapiCall();
						session.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					folder.MapiFolder.SaveRuleBatch(new Rule[]
					{
						rule
					});
					if (rule.Operation == RuleOperation.Create)
					{
						Rule[] rules = folder.MapiFolder.GetRules(new PropTag[0]);
						foreach (Rule rule2 in rules)
						{
							if (rule2.IsExtended && rule2.Name == "Never Clutter Rule")
							{
								return rule2;
							}
						}
					}
					result = rule;
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex, session, mapiThis, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ClutterOverrideManager::SaveNeverClutterRule", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex2, session, mapiThis, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("ClutterOverrideManager::SaveNeverClutterRule", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (session != null)
						{
							session.EndMapiCall();
							if (flag)
							{
								session.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0002B6CC File Offset: 0x000298CC
		internal static Rule LoadNeverClutterRule(object mapiThis, StoreSession session)
		{
			Rule result;
			using (Folder folder = Folder.Bind(session, session.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				try
				{
					Rule[] array = null;
					bool flag = false;
					try
					{
						if (session != null)
						{
							session.BeginMapiCall();
							session.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						array = folder.MapiFolder.GetRules(new PropTag[0]);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex, session, mapiThis, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("ClutterOverrideManager::LoadNeverClutterRule", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex2, session, mapiThis, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("ClutterOverrideManager::LoadNeverClutterRule", new object[0]),
							ex2
						});
					}
					finally
					{
						try
						{
							if (session != null)
							{
								session.EndMapiCall();
								if (flag)
								{
									session.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
					foreach (Rule rule in array)
					{
						if (rule.IsExtended && rule.Name == "Never Clutter Rule")
						{
							return rule;
						}
					}
				}
				catch (StoragePermanentException)
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		internal static List<SmtpAddress> LoadNeverClutterList(Rule rule)
		{
			List<SmtpAddress> list = new List<SmtpAddress>();
			if (rule != null)
			{
				Restriction.OrRestriction orRestriction = rule.Condition as Restriction.OrRestriction;
				if (orRestriction != null)
				{
					foreach (Restriction restriction in orRestriction.Restrictions)
					{
						Restriction.ContentRestriction contentRestriction = restriction as Restriction.ContentRestriction;
						if (contentRestriction != null && contentRestriction.PropTag == PropTag.SenderSmtpAddress)
						{
							string @string = contentRestriction.PropValue.GetString();
							SmtpAddress item = new SmtpAddress(@string);
							if (item.IsValidAddress)
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002B944 File Offset: 0x00029B44
		internal static PropTag GetNeverClutterTag(object mapiThis, StoreSession session)
		{
			PropertyDefinition inferenceNeverClutterOverrideApplied = ItemSchema.InferenceNeverClutterOverrideApplied;
			NamedProp namedProp = new NamedProp(WellKnownPropertySet.Inference, inferenceNeverClutterOverrideApplied.Name);
			NamedProp namedProp2 = WellKnownNamedProperties.Find(namedProp);
			NamedProp namedProp3 = namedProp2 ?? namedProp;
			NamedProp[] np = new NamedProp[]
			{
				namedProp3
			};
			PropTag[] array = null;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				array = session.Mailbox.MapiStore.GetIDsFromNames(true, np);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex, session, mapiThis, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("ClutterOverrideManager::GetNeverClutterTag", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiRulesError, ex2, session, mapiThis, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("ClutterOverrideManager::GetNeverClutterTag", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return (array.Length > 0) ? (array[0] | (PropTag)11U) : PropTag.Null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		private void Load()
		{
			this.neverClutterRule = ClutterOverrideManager.LoadNeverClutterRule(this, this.session);
			this.neverClutterSenders = ClutterOverrideManager.LoadNeverClutterList(this.neverClutterRule);
			this.markNeverClutterTag = ClutterOverrideManager.GetNeverClutterTag(this, this.session);
			if (this.neverClutterRule != null && (this.neverClutterRule.StateFlags.HasFlag(RuleStateFlags.Error) || !this.neverClutterRule.StateFlags.HasFlag(RuleStateFlags.Enabled)))
			{
				this.isDirty = true;
				return;
			}
			this.isDirty = false;
		}

		// Token: 0x0400015E RID: 350
		internal const string NeverClutterName = "Never Clutter Rule";

		// Token: 0x0400015F RID: 351
		internal const string NeverClutterRuleProvider = "NeverClutterOverrideRule";

		// Token: 0x04000160 RID: 352
		internal const int MaxSize = 1024;

		// Token: 0x04000161 RID: 353
		private readonly StoreSession session;

		// Token: 0x04000162 RID: 354
		private PropTag markNeverClutterTag;

		// Token: 0x04000163 RID: 355
		private Rule neverClutterRule;

		// Token: 0x04000164 RID: 356
		private List<SmtpAddress> neverClutterSenders;

		// Token: 0x04000165 RID: 357
		private bool isDirty;
	}
}
