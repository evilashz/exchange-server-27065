using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000025 RID: 37
	internal class AsrSearchManager : ActivityManager
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00007D82 File Offset: 0x00005F82
		internal AsrSearchManager(ActivityManager manager, AsrSearchManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007D8C File Offset: 0x00005F8C
		internal bool StarOutToDialPlanEnabled
		{
			get
			{
				SpeechAutoAttendantManager speechAutoAttendantManager = base.Manager as SpeechAutoAttendantManager;
				return speechAutoAttendantManager != null && speechAutoAttendantManager.StarOutToDialPlanEnabled;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007DB0 File Offset: 0x00005FB0
		internal string BusinessName
		{
			get
			{
				SpeechAutoAttendantManager speechAutoAttendantManager = base.Manager as SpeechAutoAttendantManager;
				if (speechAutoAttendantManager == null)
				{
					return null;
				}
				return speechAutoAttendantManager.BusinessName;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007DD4 File Offset: 0x00005FD4
		internal bool RepeatMainMenu
		{
			get
			{
				SpeechAutoAttendantManager speechAutoAttendantManager = base.Manager as SpeechAutoAttendantManager;
				return speechAutoAttendantManager == null || speechAutoAttendantManager.RepeatMainMenu;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007DF8 File Offset: 0x00005FF8
		internal AutoAttendantContext AAContext
		{
			get
			{
				SpeechAutoAttendantManager speechAutoAttendantManager = base.Manager as SpeechAutoAttendantManager;
				if (speechAutoAttendantManager == null)
				{
					return null;
				}
				return speechAutoAttendantManager.AAContext;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007E1C File Offset: 0x0000601C
		internal bool MaxPersonalContactsExceeded
		{
			get
			{
				PersonalContactsGrammarFile personalContactsGrammarFile = this.GlobalManager.PersonalContactsGrammarFile;
				return personalContactsGrammarFile != null && personalContactsGrammarFile.MaxEntriesExceeded;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007E40 File Offset: 0x00006040
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.searchContext = (AsrSearchContext)this.ReadVariable("searchContext");
			base.WriteVariable("mode", refInfo);
			this.ClearState();
			base.Start(vo, null);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007E74 File Offset: 0x00006074
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "AsrSearchManager::ExecuteAction({0}) PreviousState: {1}.", new object[]
			{
				action,
				this.menuState
			});
			string input = null;
			if (action.StartsWith("init", StringComparison.OrdinalIgnoreCase))
			{
				this.previousState = this.menuState;
				if (action != null)
				{
					if (!(action == "initAskAgainQA"))
					{
						if (!(action == "initConfirmAgainQA"))
						{
							if (!(action == "initConfirmQA"))
							{
								if (!(action == "initConfirmViaListQA"))
								{
									if (action == "initNameCollisionQA")
									{
										this.menuState = AsrSearchManager.MenuState.CollisionQA;
									}
								}
								else
								{
									this.menuState = AsrSearchManager.MenuState.ConfirmViaListQA;
									this.confirmViaListQA = true;
								}
							}
							else
							{
								this.menuState = AsrSearchManager.MenuState.ConfirmQA;
							}
						}
						else
						{
							this.menuState = AsrSearchManager.MenuState.ConfirmAgainQA;
						}
					}
					else
					{
						this.menuState = AsrSearchManager.MenuState.AskAgainQA;
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "AsrSearchManager::ExecuteAction({0}) PreviousState: {1} NextState: {2}.", new object[]
				{
					action,
					this.previousState,
					this.menuState
				});
			}
			else if (string.Equals(action, "setExtensionNumber", StringComparison.OrdinalIgnoreCase))
			{
				AsrSearchResult varValue = AsrSearchResult.Create(base.DtmfDigits);
				base.Manager.WriteVariable("searchResult", varValue);
			}
			else if (string.Equals(action, "handleRecognition", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "AsrSearchManager::HandleRecognition().", new object[0]);
				List<List<IUMRecognitionPhrase>> speechRecognitionResults = base.RecoResult.GetSpeechRecognitionResults();
				List<List<IUMRecognitionPhrase>> list = this.searchContext.ProcessMultipleResults(speechRecognitionResults);
				if (list == null || list.Count == 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "AsrSearchManager::HandleRecognition - did not find any valid results. Doing fallback.", new object[0]);
					input = "invalidSearchResult";
				}
				else
				{
					input = this.currentMenu.HandleRecognition(vo, list);
					this.searchContext.OnNameSpoken();
				}
			}
			else if (string.Equals(action, "handleYes", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleYes(vo);
			}
			else if (string.Equals(action, "handleNo", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleNo(vo);
			}
			else if (string.Equals(action, "handleNotListed", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleNotListed(vo);
			}
			else if (string.Equals(action, "handleNotSure", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleNotSure(vo);
			}
			else if (string.Equals(action, "handleChoice", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleChoice(vo);
			}
			else if (string.Equals(action, "handleDtmfChoice", StringComparison.OrdinalIgnoreCase))
			{
				input = this.currentMenu.HandleDtmfChoice(vo);
			}
			else
			{
				if (!string.Equals(action, "resetSearchState", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				this.ClearState();
				input = null;
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008128 File Offset: 0x00006328
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000812A File Offset: 0x0000632A
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			base.SetNavigationFailure("Hangup during AsrSearch");
			base.OnUserHangup(vo, voiceEventArgs);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000813F File Offset: 0x0000633F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AsrSearchManager>(this);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008147 File Offset: 0x00006347
		private void ChangeState(AsrSearchManager.MenuBase newMenu)
		{
			this.currentMenu = newMenu;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008150 File Offset: 0x00006350
		private void ClearState()
		{
			this.nbestPhase = NBestPhase.Phase1;
			this.menuState = AsrSearchManager.MenuState.OpeningMenu;
			this.previousState = AsrSearchManager.MenuState.None;
			this.confirmViaListQA = false;
			this.ChangeState(new AsrSearchManager.OpeningMenu(this, this.searchContext));
		}

		// Token: 0x04000089 RID: 137
		internal const float MaxConfidence = 1f;

		// Token: 0x0400008A RID: 138
		internal const float HighConfidence = 0.7f;

		// Token: 0x0400008B RID: 139
		internal const float LowConfidence = 0.2f;

		// Token: 0x0400008C RID: 140
		internal const float DeltaConfidence = 0.3f;

		// Token: 0x0400008D RID: 141
		private const int NumberOfResultsToShow = 9;

		// Token: 0x0400008E RID: 142
		private AsrSearchManager.MenuState menuState;

		// Token: 0x0400008F RID: 143
		private AsrSearchManager.MenuState previousState;

		// Token: 0x04000090 RID: 144
		private AsrSearchManager.MenuBase currentMenu;

		// Token: 0x04000091 RID: 145
		private AsrSearchContext searchContext;

		// Token: 0x04000092 RID: 146
		private NBestPhase nbestPhase;

		// Token: 0x04000093 RID: 147
		private bool confirmViaListQA;

		// Token: 0x02000026 RID: 38
		internal enum MenuState
		{
			// Token: 0x04000095 RID: 149
			None,
			// Token: 0x04000096 RID: 150
			OpeningMenu,
			// Token: 0x04000097 RID: 151
			ConfirmQA,
			// Token: 0x04000098 RID: 152
			ConfirmAgainQA,
			// Token: 0x04000099 RID: 153
			ConfirmViaListQA,
			// Token: 0x0400009A RID: 154
			AskAgainQA,
			// Token: 0x0400009B RID: 155
			CollisionQA
		}

		// Token: 0x02000027 RID: 39
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x060001AC RID: 428 RVA: 0x00008180 File Offset: 0x00006380
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x060001AD RID: 429 RVA: 0x00008189 File Offset: 0x00006389
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing ASR search activity manager.", new object[0]);
				return new AsrSearchManager(manager, this);
			}
		}

		// Token: 0x02000028 RID: 40
		private abstract class MenuBase
		{
			// Token: 0x060001AE RID: 430 RVA: 0x000081A8 File Offset: 0x000063A8
			protected MenuBase(AsrSearchManager manager, AsrSearchContext context)
			{
				this.manager = manager;
				this.context = context;
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060001AF RID: 431 RVA: 0x000081BE File Offset: 0x000063BE
			protected AsrSearchManager Manager
			{
				get
				{
					return this.manager;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x000081C6 File Offset: 0x000063C6
			protected AsrSearchContext Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x000081D0 File Offset: 0x000063D0
			internal static List<List<IUMRecognitionPhrase>> ConvertToListOfLists(List<IUMRecognitionPhrase> results)
			{
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				foreach (IUMRecognitionPhrase item in results)
				{
					list.Add(new List<IUMRecognitionPhrase>
					{
						item
					});
				}
				return list;
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x00008234 File Offset: 0x00006434
			internal virtual void Initialize(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Initialize", new object[0]);
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x0000824C File Offset: 0x0000644C
			internal virtual string HandleYes(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "ENTER: HandleYes(context.ResultsToPlay.Count={0}[{1}]).", new object[]
				{
					this.Context.ResultsToPlay.Count,
					this.Context.ResultsToPlay[0].Count
				});
				return this.CommonYesHandler(vo, this.Context.ResultsToPlay[0]);
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x000082BF File Offset: 0x000064BF
			internal virtual string HandleNo(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleNo().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x000082DC File Offset: 0x000064DC
			internal virtual string HandleNotSure(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleNotSure().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x000082F9 File Offset: 0x000064F9
			internal virtual string HandleNotListed(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleNotListed().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00008316 File Offset: 0x00006516
			internal virtual string HandleRecognition(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleRecognition().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x00008333 File Offset: 0x00006533
			internal virtual string HandleChoice(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x00008350 File Offset: 0x00006550
			internal virtual string HandleDtmfChoice(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleDtmfChoice().", new object[0]);
				throw new NotImplementedException();
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00008370 File Offset: 0x00006570
			internal virtual string CommonNameCollisionHandler(List<IUMRecognitionPhrase> alternates)
			{
				string text;
				if (alternates.Count > Constants.DirectorySearch.MaxResultsToDisplay)
				{
					text = "resultsMoreThanAllowed";
				}
				else if (this.Context.CanShowExactMatches())
				{
					this.Manager.ChangeState(new AsrSearchManager.CollisionQA(this.Manager, this.Context));
					text = "collision";
					this.Context.PrepareForCollisionQA(AsrSearchManager.MenuBase.ConvertToListOfLists(alternates));
				}
				else
				{
					this.Context.PrepareForPromptForAliasQA(alternates);
					this.Manager.ChangeState(new AsrSearchManager.PromptForAliasQA(this.Manager, this.Context));
					text = "promptForAlias";
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonNameCollisionHandler(#homohones = {0}) returning autoEvent {1}.", new object[]
				{
					alternates.Count,
					text
				});
				return text;
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008430 File Offset: 0x00006630
			internal virtual string CommonConfirmViaListHandler(List<List<IUMRecognitionPhrase>> alternates)
			{
				string text;
				if (this.Context.CanShowExactMatches())
				{
					if (alternates[0].Count > Constants.DirectorySearch.MaxResultsToDisplay)
					{
						text = "resultsMoreThanAllowed";
						CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonConfirmViaListHandler(alternates[0].Count = {0}) returning autoEvent {1}.", new object[]
						{
							alternates[0].Count,
							text
						});
						return text;
					}
					List<List<IUMRecognitionPhrase>> results = AsrSearchManager.MenuBase.FlattenList(alternates);
					this.Context.PrepareForConfirmViaListQA(results);
					this.Manager.ChangeState(new AsrSearchManager.ConfirmViaListQA(this.Manager, this.Context));
					text = "confirmViaList";
				}
				else
				{
					this.Context.PrepareForConfirmViaListQA(alternates);
					this.Manager.ChangeState(new AsrSearchManager.ConfirmViaListQA(this.Manager, this.Context));
					text = "confirmViaList";
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonConfirmViaListHandler(#homophones = {0}) returning autoEvent {1}.", new object[]
				{
					alternates.Count,
					text
				});
				return text;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x0000852C File Offset: 0x0000672C
			protected static List<List<IUMRecognitionPhrase>> GetTopNEntries(List<List<IUMRecognitionPhrase>> alternates, int n)
			{
				int num = 0;
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				foreach (List<IUMRecognitionPhrase> item in alternates)
				{
					num++;
					if (num > n)
					{
						break;
					}
					list.Add(item);
				}
				return list;
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000858C File Offset: 0x0000678C
			protected static bool TryParseBookmark(string bookmark, out int index)
			{
				index = -1;
				bool result = false;
				if (bookmark != null && bookmark.StartsWith("user", StringComparison.Ordinal))
				{
					string s = bookmark.Substring(4);
					result = int.TryParse(s, out index);
				}
				return result;
			}

			// Token: 0x060001BE RID: 446 RVA: 0x000085C0 File Offset: 0x000067C0
			protected string DoNBest_Phase2(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "DoNBest_Phase2() alternates.Count = {0}.", new object[]
				{
					(alternates != null) ? alternates.Count : -1
				});
				this.Context.PrepareForNBestPhase2();
				string result = null;
				List<List<IUMRecognitionPhrase>> list = this.RemoveRejectedPhaseOneNames(alternates);
				if (list.Count == 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "No results after removing rejected phase-1 names. Returning to search opening menu.", new object[0]);
					return "retrySearch";
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "DoNBest_Phase2() currentResults.Count = {0}.", new object[]
				{
					list.Count
				});
				list.Sort(RecognitionPhraseListComparer.StaticInstance);
				if (this.Manager.confirmViaListQA)
				{
					List<List<IUMRecognitionPhrase>> topNEntries = AsrSearchManager.MenuBase.GetTopNEntries(list, 1);
					this.Manager.ChangeState(new AsrSearchManager.ConfirmAgainQA(this.Manager, this.Context));
					if (!this.Context.PrepareForConfirmAgainQA(topNEntries))
					{
						result = "invalidSearchResult";
					}
					return result;
				}
				List<List<IUMRecognitionPhrase>> resultsInConfidenceRange = AsrSearchManager.MenuBase.GetResultsInConfidenceRange(list, 0.7f, 1f);
				if (resultsInConfidenceRange.Count > 0)
				{
					List<List<IUMRecognitionPhrase>> topNEntries2 = AsrSearchManager.MenuBase.GetTopNEntries(resultsInConfidenceRange, 9);
					if (topNEntries2.Count > 1)
					{
						return this.CommonConfirmViaListHandler(topNEntries2);
					}
					if (!this.Context.PrepareForConfirmAgainQA(topNEntries2))
					{
						result = "invalidSearchResult";
					}
					this.Manager.ChangeState(new AsrSearchManager.ConfirmAgainQA(this.Manager, this.Context));
					return result;
				}
				else
				{
					List<List<IUMRecognitionPhrase>> resultsInConfidenceRange2 = AsrSearchManager.MenuBase.GetResultsInConfidenceRange(list, 0.2f, 0.7f);
					if (resultsInConfidenceRange2.Count > 0)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Got Medium confidence results (count={0}), working with the topmost result.", new object[]
						{
							resultsInConfidenceRange2.Count
						});
						list = resultsInConfidenceRange2.GetRange(0, 1);
						if (!this.Context.PrepareForConfirmAgainQA(list))
						{
							result = "invalidSearchResult";
						}
						this.Manager.ChangeState(new AsrSearchManager.ConfirmAgainQA(this.Manager, this.Context));
						return result;
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Phase2 processing did not return any results for the user. Doing fallback processing.", new object[0]);
					return "doFallback";
				}
			}

			// Token: 0x060001BF RID: 447 RVA: 0x000087BC File Offset: 0x000069BC
			protected string CommonYesHandler(BaseUMCallSession vo, List<IUMRecognitionPhrase> resultList)
			{
				string text = null;
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonYesHandler(): resultList.Count: {0}.", new object[]
				{
					resultList.Count
				});
				if (this.Context.ResultsToPlay.Count == 1 && resultList.Count == 1)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonYesHandler(): have one result in ResultsToPlay. Going to transfer.", new object[0]);
					AsrSearchResult varValue = AsrSearchResult.Create(resultList[0], vo.CurrentCallContext.CallerInfo, vo.CurrentCallContext.TenantGuid);
					this.Manager.Manager.WriteVariable("searchResult", varValue);
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "CommonYesHandler(): have more than one result in ResultsToPlay. Going to NameCollision.", new object[0]);
					text = this.CommonNameCollisionHandler(resultList);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "LEAVE: CommonYesHandler() returning autoEvent = {0}.", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x0000889C File Offset: 0x00006A9C
			private static List<List<IUMRecognitionPhrase>> FlattenList(List<List<IUMRecognitionPhrase>> alternates)
			{
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				int num = Constants.DirectorySearch.MaxResultsToDisplay;
				int num2 = 0;
				while (num2 < alternates.Count && alternates[num2].Count <= num)
				{
					for (int i = 0; i < alternates[num2].Count; i++)
					{
						list.Add(new List<IUMRecognitionPhrase>
						{
							alternates[num2][i]
						});
						num--;
						if (num == 0)
						{
							break;
						}
					}
					if (num == 0)
					{
						break;
					}
					num2++;
				}
				return list;
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x0000891A File Offset: 0x00006B1A
			private static bool IsPhraseInConfidenceRange(IUMRecognitionPhrase phrase, float low, float high)
			{
				return phrase.Confidence >= low && phrase.Confidence <= high;
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00008934 File Offset: 0x00006B34
			private static List<List<IUMRecognitionPhrase>> GetResultsInConfidenceRange(List<List<IUMRecognitionPhrase>> alternates, float low, float high)
			{
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				foreach (List<IUMRecognitionPhrase> list2 in alternates)
				{
					IUMRecognitionPhrase iumrecognitionPhrase = list2[0];
					if (iumrecognitionPhrase != null && AsrSearchManager.MenuBase.IsPhraseInConfidenceRange(iumrecognitionPhrase, low, high))
					{
						list.Add(list2);
					}
				}
				return list;
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x000089A0 File Offset: 0x00006BA0
			private List<List<IUMRecognitionPhrase>> RemoveRejectedPhaseOneNames(List<List<IUMRecognitionPhrase>> alternates)
			{
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				Dictionary<string, IUMRecognitionPhrase> dictionary = new Dictionary<string, IUMRecognitionPhrase>();
				foreach (List<IUMRecognitionPhrase> list2 in this.Context.RejectedResults)
				{
					foreach (IUMRecognitionPhrase iumrecognitionPhrase in list2)
					{
						string text = (string)iumrecognitionPhrase["ResultType"];
						string a;
						if ((a = text) != null)
						{
							if (!(a == "DirectoryContact"))
							{
								if (!(a == "PersonalContact"))
								{
									if (a == "Department")
									{
										string key = (string)iumrecognitionPhrase["DepartmentName"];
										if (!dictionary.ContainsKey(key))
										{
											dictionary.Add(key, iumrecognitionPhrase);
										}
									}
								}
								else
								{
									string key2 = (string)iumrecognitionPhrase["ContactId"];
									if (!dictionary.ContainsKey(key2))
									{
										dictionary.Add(key2, iumrecognitionPhrase);
									}
								}
							}
							else
							{
								string key3 = (string)iumrecognitionPhrase["ObjectGuid"];
								if (!dictionary.ContainsKey(key3))
								{
									dictionary.Add(key3, iumrecognitionPhrase);
								}
							}
						}
					}
				}
				foreach (List<IUMRecognitionPhrase> list3 in alternates)
				{
					bool flag = false;
					List<IUMRecognitionPhrase> list4 = null;
					foreach (IUMRecognitionPhrase iumrecognitionPhrase2 in list3)
					{
						string text2 = (string)iumrecognitionPhrase2["ResultType"];
						string a2;
						if ((a2 = text2) != null)
						{
							if (!(a2 == "DirectoryContact"))
							{
								if (!(a2 == "PersonalContact"))
								{
									if (a2 == "Department")
									{
										string text3 = (string)iumrecognitionPhrase2["DepartmentName"];
										flag = dictionary.ContainsKey(text3);
										CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Department: {0} Rejected: {1}.", new object[]
										{
											text3,
											flag
										});
									}
								}
								else
								{
									string text4 = (string)iumrecognitionPhrase2["ContactId"];
									flag = dictionary.ContainsKey(text4);
									PIIMessage data = PIIMessage.Create(PIIType._PII, text4);
									CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, data, "ContactId: _PII Rejected: {0}.", new object[]
									{
										flag
									});
								}
							}
							else
							{
								string text5 = (string)iumrecognitionPhrase2["ObjectGuid"];
								string value = (string)iumrecognitionPhrase2["SMTP"];
								flag = dictionary.ContainsKey(text5);
								PIIMessage data2 = PIIMessage.Create(PIIType._EmailAddress, value);
								CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, data2, "Guid: {0} Email:_EmailAddress Rejected: {1}.", new object[]
								{
									text5,
									flag
								});
							}
						}
						if (!flag)
						{
							if (list4 == null)
							{
								list4 = new List<IUMRecognitionPhrase>();
							}
							list4.Add(iumrecognitionPhrase2);
						}
					}
					if (list4 != null && list4.Count > 0)
					{
						list.Add(list4);
					}
				}
				return list;
			}

			// Token: 0x0400009C RID: 156
			private AsrSearchManager manager;

			// Token: 0x0400009D RID: 157
			private AsrSearchContext context;
		}

		// Token: 0x02000029 RID: 41
		private class OpeningMenu : AsrSearchManager.MenuBase
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x00008D38 File Offset: 0x00006F38
			internal OpeningMenu(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00008D44 File Offset: 0x00006F44
			internal override string HandleRecognition(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "RecoResult = {0}[{1}].", new object[]
				{
					base.Manager.RecoResult.Confidence,
					base.Manager.RecoResult.Text
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Alternates.Count = {0}.", new object[]
				{
					alternates.Count
				});
				return this.DoNBest_Phase1(vo, alternates);
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00008DC4 File Offset: 0x00006FC4
			internal string DoNBest_Phase1(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "NBest_Phase1::(Alternates.Count == {0}).", new object[]
				{
					alternates.Count
				});
				List<List<IUMRecognitionPhrase>> list = null;
				if (alternates.Count > 1)
				{
					list = this.GetResultsInDeltaConfidenceRange(alternates, 0.3f);
				}
				List<List<IUMRecognitionPhrase>> topNEntries;
				if (list != null && list.Count > 0)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "NBest_Phase1::Got Delta Confidence Results, Count == {0}.", new object[]
					{
						list.Count
					});
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Getting max: {0} results from the deltaconfidence results", new object[]
					{
						9
					});
					topNEntries = AsrSearchManager.MenuBase.GetTopNEntries(list, 9);
				}
				else
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Did not get deltaconfidence results, just getting the top 1.", new object[0]);
					topNEntries = AsrSearchManager.MenuBase.GetTopNEntries(alternates, 1);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "NBest_Phase1::Final Results (count = {0}).", new object[]
				{
					topNEntries.Count
				});
				string result;
				if (topNEntries.Count > 1)
				{
					result = this.CommonConfirmViaListHandler(topNEntries);
				}
				else
				{
					result = null;
					base.Context.ResultsToPlay = topNEntries.GetRange(0, 1);
					base.Manager.ChangeState(new AsrSearchManager.ConfirmQA(base.Manager, base.Context));
					if (!base.Context.PrepareForConfirmQA(base.Context.ResultsToPlay))
					{
						result = "invalidSearchResult";
					}
				}
				return result;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00008F24 File Offset: 0x00007124
			protected List<List<IUMRecognitionPhrase>> GetResultsInDeltaConfidenceRange(List<List<IUMRecognitionPhrase>> alternates, float deltaConfidence)
			{
				List<List<IUMRecognitionPhrase>> list = new List<List<IUMRecognitionPhrase>>();
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "GetResultsInDeltaConfidenceRange(alternates.Count = {0}, deltaConf = {1}).", new object[]
				{
					alternates.Count,
					deltaConfidence
				});
				float num = -1f;
				float num2 = -1f;
				float num3 = -1f;
				foreach (List<IUMRecognitionPhrase> list2 in alternates)
				{
					IUMRecognitionPhrase iumrecognitionPhrase = list2[0];
					PIIMessage data = PIIMessage.Create(PIIType._PII, iumrecognitionPhrase.Text);
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, data, "Root Phrase: {0}[_PII] Alternates.Count ={1}.", new object[]
					{
						iumrecognitionPhrase.Confidence,
						list2.Count
					});
					if (num == -1f)
					{
						num = iumrecognitionPhrase.Confidence;
						num2 = num + deltaConfidence;
						num2 = Math.Min(num2, 1f);
						num3 = Math.Max(0f, num - deltaConfidence);
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, data, "Phrase: {0}[_PII] Expected Range ={1}-{2}.", new object[]
					{
						iumrecognitionPhrase.Confidence,
						num3,
						num2
					});
					if (iumrecognitionPhrase.Confidence >= num3 && iumrecognitionPhrase.Confidence <= num2)
					{
						list.Add(list2);
					}
				}
				return list;
			}
		}

		// Token: 0x0200002A RID: 42
		private class ConfirmQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001C8 RID: 456 RVA: 0x000090A0 File Offset: 0x000072A0
			internal ConfirmQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x000090AC File Offset: 0x000072AC
			internal override string HandleRecognition(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "ConfirmQA::handleNo - setting rejectedResults (count = {0}).", new object[]
				{
					base.Context.ResultsToPlay.Count
				});
				base.Manager.nbestPhase = NBestPhase.Phase2;
				base.Context.RejectedResults = base.Context.ResultsToPlay;
				return base.DoNBest_Phase2(vo, alternates);
			}

			// Token: 0x060001CA RID: 458 RVA: 0x00009115 File Offset: 0x00007315
			internal override string HandleYes(BaseUMCallSession vo)
			{
				return base.HandleYes(vo);
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00009120 File Offset: 0x00007320
			internal override string HandleNo(BaseUMCallSession vo)
			{
				base.Context.RejectedResults = base.Context.ResultsToPlay;
				base.Context.ResultsToPlay = null;
				base.Manager.ChangeState(new AsrSearchManager.AskAgainQA(base.Manager, base.Context));
				return null;
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000916C File Offset: 0x0000736C
			internal override string HandleNotSure(BaseUMCallSession vo)
			{
				return "doFallback";
			}
		}

		// Token: 0x0200002B RID: 43
		private class ConfirmAgainQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001CD RID: 461 RVA: 0x00009180 File Offset: 0x00007380
			internal ConfirmAgainQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001CE RID: 462 RVA: 0x0000918A File Offset: 0x0000738A
			internal override string HandleYes(BaseUMCallSession vo)
			{
				return base.HandleYes(vo);
			}

			// Token: 0x060001CF RID: 463 RVA: 0x00009194 File Offset: 0x00007394
			internal override string HandleNo(BaseUMCallSession vo)
			{
				return "doFallback";
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x000091A8 File Offset: 0x000073A8
			internal override string HandleNotSure(BaseUMCallSession vo)
			{
				return "doFallback";
			}
		}

		// Token: 0x0200002C RID: 44
		private class ConfirmViaListQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001D1 RID: 465 RVA: 0x000091BC File Offset: 0x000073BC
			internal ConfirmViaListQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x000091C6 File Offset: 0x000073C6
			internal override void Initialize(BaseUMCallSession vo)
			{
				base.Manager.confirmViaListQA = true;
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x000091D4 File Offset: 0x000073D4
			internal override string HandleNotListed(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Enter:HandleNotListed().", new object[0]);
				string text = null;
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int num = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out num))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() bookmark \"{0}\" was invalid.", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleNotListed(): User said Not Listed while list was being played. Stopping playback.", new object[0]);
					vo.StopPlayback();
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() returning AutoEvent: <null>", new object[0]);
					return null;
				}
				switch (base.Manager.nbestPhase)
				{
				case NBestPhase.Phase1:
					base.Context.RejectedResults = base.Context.ResultsToPlay;
					base.Context.ResultsToPlay = null;
					text = "doAskAgainQA";
					base.Manager.ChangeState(new AsrSearchManager.AskAgainQA(base.Manager, base.Context));
					break;
				case NBestPhase.Phase2:
					text = "doFallback";
					break;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() returning AutoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x000092F8 File Offset: 0x000074F8
			internal override string HandleYes(BaseUMCallSession vo)
			{
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int choiceIndex = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out choiceIndex))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleYes() bookmark \"{0}\" was invalid", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleYes(): User said Yes while list was being played. Stopping playback", new object[0]);
					vo.StopPlayback();
					string text = null;
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleYes() returning AutoEvent: {0}", new object[]
					{
						text
					});
					return text;
				}
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x0000938C File Offset: 0x0000758C
			internal override string HandleChoice(BaseUMCallSession vo)
			{
				string s = base.Manager.RecoResult["Choice"] as string;
				int choiceIndex = int.Parse(s, CultureInfo.InvariantCulture);
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x000093C8 File Offset: 0x000075C8
			internal override string HandleDtmfChoice(BaseUMCallSession vo)
			{
				int choiceIndex = int.Parse(base.Manager.DtmfDigits, CultureInfo.InvariantCulture);
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x000093F4 File Offset: 0x000075F4
			internal override string HandleNo(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Enter:HandleNo().", new object[0]);
				string text = null;
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int num = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out num))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() bookmark \"{0}\" was invalid.", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() returning AutoEvent: <null>", new object[0]);
					return null;
				}
				switch (base.Manager.nbestPhase)
				{
				case NBestPhase.Phase1:
					base.Context.RejectedResults = base.Context.ResultsToPlay;
					base.Context.ResultsToPlay = null;
					text = "doAskAgainQA";
					base.Manager.ChangeState(new AsrSearchManager.AskAgainQA(base.Manager, base.Context));
					break;
				case NBestPhase.Phase2:
					text = "doFallback";
					break;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() returning AutoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x000094FC File Offset: 0x000076FC
			private string HandleChoice(BaseUMCallSession vo, int choiceIndex)
			{
				choiceIndex--;
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice: User Input: {0}.", new object[]
				{
					choiceIndex
				});
				if (choiceIndex >= base.Context.ResultsToPlay.Count)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Choice entered was wrong...", new object[0]);
					return "invalidSelection";
				}
				List<IUMRecognitionPhrase> list = base.Context.ResultsToPlay[choiceIndex];
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice: Alternates.Count: {0}.", new object[]
				{
					list.Count
				});
				string text;
				if (list.Count == 1)
				{
					AsrSearchResult varValue = AsrSearchResult.Create(list[0], vo.CurrentCallContext.CallerInfo, vo.CurrentCallContext.TenantGuid);
					base.Manager.Manager.WriteVariable("searchResult", varValue);
					text = "validChoice";
				}
				else
				{
					text = this.CommonNameCollisionHandler(list);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice returning autoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}
		}

		// Token: 0x0200002D RID: 45
		private class AskAgainQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x0000960D File Offset: 0x0000780D
			internal AskAgainQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00009617 File Offset: 0x00007817
			internal override string HandleRecognition(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				base.Manager.nbestPhase = NBestPhase.Phase2;
				return base.DoNBest_Phase2(vo, alternates);
			}

			// Token: 0x060001DB RID: 475 RVA: 0x00009630 File Offset: 0x00007830
			internal override string HandleNo(BaseUMCallSession vo)
			{
				return "doFallback";
			}
		}

		// Token: 0x0200002E RID: 46
		private class CollisionQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001DC RID: 476 RVA: 0x00009644 File Offset: 0x00007844
			internal CollisionQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001DD RID: 477 RVA: 0x00009650 File Offset: 0x00007850
			internal override string HandleNotListed(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Enter:HandleNotListed().", new object[0]);
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int num = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out num))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() bookmark \"{0}\" was invalid.", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleNotListed(): User said Not Listed while list was being played. Stopping playback.", new object[0]);
					vo.StopPlayback();
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() returning AutoEvent: <null>", new object[0]);
					return null;
				}
				string text = "doFallback";
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNotListed() returning AutoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00009710 File Offset: 0x00007910
			internal override string HandleYes(BaseUMCallSession vo)
			{
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int choiceIndex = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out choiceIndex))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleYes() bookmark \"{0}\" was invalid", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleYes(): User said Yes/Number-X at end of list. Stopping playback", new object[0]);
					vo.StopPlayback();
					string text = null;
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleYes() returning AutoEvent: {0}", new object[]
					{
						text
					});
					return text;
				}
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001DF RID: 479 RVA: 0x000097A4 File Offset: 0x000079A4
			internal override string HandleNo(BaseUMCallSession vo)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Enter:HandleNo().", new object[0]);
				string lastBookmarkReached = base.Manager.LastBookmarkReached;
				int num = -1;
				if (!AsrSearchManager.MenuBase.TryParseBookmark(lastBookmarkReached, out num))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() bookmark \"{0}\" was invalid.", new object[]
					{
						lastBookmarkReached
					});
					return null;
				}
				if (vo.IsDuringPlayback())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() returning AutoEvent: <null>", new object[0]);
					return null;
				}
				string text = "doFallback";
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Exit:HandleNo() returning AutoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x00009848 File Offset: 0x00007A48
			internal override string HandleChoice(BaseUMCallSession vo)
			{
				string s = base.Manager.RecoResult["Choice"] as string;
				int choiceIndex = int.Parse(s, CultureInfo.InvariantCulture);
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x00009884 File Offset: 0x00007A84
			internal override string HandleDtmfChoice(BaseUMCallSession vo)
			{
				int choiceIndex = int.Parse(base.Manager.DtmfDigits, CultureInfo.InvariantCulture);
				return this.HandleChoice(vo, choiceIndex);
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x000098B0 File Offset: 0x00007AB0
			private string HandleChoice(BaseUMCallSession vo, int choiceIndex)
			{
				string text = "validChoice";
				choiceIndex--;
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice: User Input: {0}.", new object[]
				{
					choiceIndex
				});
				if (choiceIndex >= base.Context.ResultsToPlay.Count)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Choice entered was wrong...", new object[0]);
					return "invalidSelection";
				}
				List<IUMRecognitionPhrase> list = base.Context.ResultsToPlay[choiceIndex];
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice: Alternates.Count: {0}.", new object[]
				{
					list.Count
				});
				AsrSearchResult varValue = AsrSearchResult.Create(list[0], vo.CurrentCallContext.CallerInfo, vo.CurrentCallContext.TenantGuid);
				base.Manager.Manager.WriteVariable("searchResult", varValue);
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "HandleChoice returning autoEvent: {0}.", new object[]
				{
					text
				});
				return text;
			}
		}

		// Token: 0x0200002F RID: 47
		private class PromptForAliasQA : AsrSearchManager.MenuBase
		{
			// Token: 0x060001E3 RID: 483 RVA: 0x000099AC File Offset: 0x00007BAC
			internal PromptForAliasQA(AsrSearchManager manager, AsrSearchContext context) : base(manager, context)
			{
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x000099B8 File Offset: 0x00007BB8
			internal override string HandleRecognition(BaseUMCallSession vo, List<List<IUMRecognitionPhrase>> alternates)
			{
				string text = null;
				PIIMessage data = PIIMessage.Create(PIIType._PII, base.Manager.RecoResult.Text);
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, data, "RecoResult = {0}[_PII].", new object[]
				{
					base.Manager.RecoResult.Confidence
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "Alternates.Count = {0}.", new object[]
				{
					alternates.Count
				});
				if (alternates.Count > 1)
				{
					text = "resultsMoreThanAllowed";
					CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "PromptForAliasQA::HandleRecognition() returning autoEvent = {0}", new object[]
					{
						text
					});
					return text;
				}
				base.Context.ResultsToPlay = alternates.GetRange(0, 1);
				base.Manager.ChangeState(new AsrSearchManager.ConfirmQA(base.Manager, base.Context));
				if (!base.Context.PrepareForConfirmQA(base.Context.ResultsToPlay))
				{
					text = "invalidSearchResult";
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AsrSearchTracer, this, "PromptForAliasQA::HandleRecognition() returning autoEvent = {0}", new object[]
				{
					text
				});
				return text;
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x00009AD2 File Offset: 0x00007CD2
			internal override string HandleYes(BaseUMCallSession vo)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x00009AD9 File Offset: 0x00007CD9
			internal override string HandleChoice(BaseUMCallSession vo)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00009AE0 File Offset: 0x00007CE0
			internal override string HandleDtmfChoice(BaseUMCallSession vo)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00009AE7 File Offset: 0x00007CE7
			internal override string HandleNotListed(BaseUMCallSession vo)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x00009AEE File Offset: 0x00007CEE
			internal override string HandleNo(BaseUMCallSession vo)
			{
				throw new NotImplementedException();
			}
		}
	}
}
