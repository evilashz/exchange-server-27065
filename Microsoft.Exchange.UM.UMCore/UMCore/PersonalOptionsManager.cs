using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200019E RID: 414
	internal class PersonalOptionsManager : ActivityManager
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00034DF0 File Offset: 0x00032FF0
		internal PersonalOptionsManager(ActivityManager manager, PersonalOptionsManager.ConfigClass config) : base(manager, config)
		{
			this.TimeZoneIndex = -1;
			base.WriteVariable("greeting", null);
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00034E10 File Offset: 0x00033010
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00034E34 File Offset: 0x00033034
		private int TimeZoneIndex
		{
			get
			{
				object obj = this.ReadVariable("timeZoneIndex");
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				base.WriteVariable("timeZoneIndex", value);
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00034E48 File Offset: 0x00033048
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			CultureInfo telephonyCulture = callerInfo.TelephonyCulture;
			base.WriteVariable("Oof", callerInfo.ConfigFolder.IsOof);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = callerInfo.CreateSessionLock())
			{
				base.WriteVariable("emailOof", CommonUtil.GetEmailOOFStatus(mailboxSessionLock.Session));
			}
			base.WriteVariable("timeFormat24", CommonUtil.Is24HourTimeFormat(telephonyCulture.DateTimeFormat.ShortTimePattern));
			string text;
			string text2;
			CommonUtil.GetStandardTimeFormats(telephonyCulture, out text, out text2);
			base.WriteVariable("canToggleTimeFormat", text != null && text2 != null);
			ADUser aduser = (ADUser)callerInfo.ADRecipient;
			bool flag = callerInfo.IsASREnabled && Util.IsSpeechCulture(callerInfo.TelephonyCulture);
			base.WriteVariable("canToggleASR", flag);
			base.Start(vo, refInfo);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00034F48 File Offset: 0x00033148
		internal override void CheckAuthorization(UMSubscriber u)
		{
			if (!u.IsAuthenticated || (this.GlobalManager.LimitedOVAAccess && !u.ConfigFolder.IsFirstTimeUser))
			{
				base.CheckAuthorization(u);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00034F74 File Offset: 0x00033174
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Personal Options Manager asked to do action {0}.", new object[]
			{
				action
			});
			string input = null;
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			GreetingBase greetingBase = null;
			TransitionBase transition;
			try
			{
				if (PersonalOptionsManager.IsFetchAction(action, callerInfo, out greetingBase, out input))
				{
					if (this.FetchGreeting(greetingBase))
					{
						input = null;
					}
				}
				else if (PersonalOptionsManager.IsSaveAction(action, callerInfo, out greetingBase))
				{
					this.SaveGreeting(greetingBase);
				}
				else if (PersonalOptionsManager.IsDeleteAction(action, callerInfo, out greetingBase))
				{
					greetingBase.Delete();
				}
				else if (string.Equals(action, "validatePassword", StringComparison.OrdinalIgnoreCase))
				{
					input = this.ValidatePassword(callerInfo, vo);
				}
				else if (string.Equals(action, "matchPasswords", StringComparison.OrdinalIgnoreCase))
				{
					input = this.MatchPasswords(callerInfo, vo);
				}
				else if (string.Equals(action, "getSystemTask", StringComparison.OrdinalIgnoreCase))
				{
					if (this.systemTaskContext == null)
					{
						this.systemTaskContext = new PersonalOptionsManager.SystemTaskContext(vo.CurrentCallContext.CallerInfo, base.Manager);
					}
					bool flag = (bool)this.GlobalManager.ReadVariable("skipPinCheck");
					if (this.systemTaskContext.IsFirstTimeUserTask)
					{
						input = "firstTimeUserTask";
					}
					else if (this.systemTaskContext.IsChangePasswordTask && !flag)
					{
						input = "changePasswordTask";
					}
					else if (this.systemTaskContext.IsOofStatusTask)
					{
						input = "oofStatusTask";
					}
					base.WriteVariable("adminMinPwdLen", callerInfo.PasswordPolicy.MinimumLength);
					base.WriteVariable("adminOldPwdLen", callerInfo.PasswordPolicy.PreviousPasswordsDisallowed);
				}
				else if (string.Equals(action, "getFirstTimeUserTask", StringComparison.OrdinalIgnoreCase))
				{
					if (this.firstTimeUserContext == null)
					{
						this.firstTimeUserContext = new PersonalOptionsManager.FirstTimeUserContext(vo.CurrentCallContext.CallerInfo);
					}
					input = this.GetNextFirstTimeUserTask();
				}
				else if (string.Equals(action, "firstTimeUserComplete", StringComparison.OrdinalIgnoreCase))
				{
					callerInfo.ConfigFolder.IsFirstTimeUser = false;
					callerInfo.ConfigFolder.Save();
				}
				else if (string.Equals(action, "toggleOOF", StringComparison.OrdinalIgnoreCase))
				{
					this.ToggleOOF(vo);
				}
				else if (string.Equals(action, "toggleEmailOOF", StringComparison.OrdinalIgnoreCase))
				{
					this.ToggleEmailOOF(vo);
				}
				else if (string.Equals(action, "toggleTimeFormat", StringComparison.OrdinalIgnoreCase))
				{
					this.ToggleTimeFormat(vo);
				}
				else if (string.Equals(action, "toggleASR", StringComparison.OrdinalIgnoreCase))
				{
					this.ToggleASR(vo);
				}
				else if (string.Equals(action, "findTimeZone", StringComparison.OrdinalIgnoreCase))
				{
					input = this.FindTimeZone();
				}
				else if (string.Equals(action, "nextTimeZone", StringComparison.OrdinalIgnoreCase))
				{
					input = this.NextTimeZone();
				}
				else if (string.Equals(action, "selectTimeZone", StringComparison.OrdinalIgnoreCase))
				{
					input = this.SelectTimeZone(vo);
				}
				else if (string.Equals(action, "firstTimeZone", StringComparison.OrdinalIgnoreCase))
				{
					this.FirstTimeZone();
				}
				else
				{
					if (!string.Equals(action, "setGreetingsAction", StringComparison.OrdinalIgnoreCase))
					{
						return base.ExecuteAction(action, vo);
					}
					base.WriteVariable("lastAction", PersonalOptionsManager.LastAction.Greetings.ToString());
				}
				transition = base.CurrentActivity.GetTransition(input);
			}
			finally
			{
				if (greetingBase != null)
				{
					greetingBase.Dispose();
				}
			}
			return transition;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003528C File Offset: 0x0003348C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PersonalOptionsManager>(this);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00035294 File Offset: 0x00033494
		private static bool IsFetchAction(string action, UMSubscriber user, out GreetingBase greeting, out string autoEvent)
		{
			greeting = null;
			autoEvent = null;
			if (string.Equals(action, "getExternal", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail);
				autoEvent = "noExternal";
				return true;
			}
			if (string.Equals(action, "getOof", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away);
				autoEvent = "noOof";
				return true;
			}
			if (string.Equals(action, "getName", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenNameGreeting();
				autoEvent = "noName";
				return true;
			}
			return false;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00035318 File Offset: 0x00033518
		private static bool IsSaveAction(string action, UMSubscriber user, out GreetingBase greeting)
		{
			greeting = null;
			if (string.Equals(action, "saveExternal", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail);
				return true;
			}
			if (string.Equals(action, "saveOof", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away);
				return true;
			}
			if (string.Equals(action, "saveName", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenNameGreeting();
				return true;
			}
			return false;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00035384 File Offset: 0x00033584
		private static bool IsDeleteAction(string action, UMSubscriber user, out GreetingBase greeting)
		{
			greeting = null;
			if (string.Equals(action, "deleteExternal", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail);
				return true;
			}
			if (string.Equals(action, "deleteOof", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Away);
				return true;
			}
			if (string.Equals(action, "deleteName", StringComparison.OrdinalIgnoreCase))
			{
				greeting = user.ConfigFolder.OpenNameGreeting();
				return true;
			}
			return false;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000353F0 File Offset: 0x000335F0
		private string SelectTimeZone(BaseUMCallSession vo)
		{
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			string text = (string)this.ReadVariable("lastInput");
			if (string.IsNullOrEmpty(text))
			{
				return "invalidTimeZone";
			}
			int num = int.Parse(text, CultureInfo.InvariantCulture) - 1;
			if (num < 0 || num >= this.timeZoneList.Count)
			{
				return "invalidTimeZone";
			}
			ExTimeZone exTimeZone = this.timeZoneList[num];
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = callerInfo.CreateSessionLock())
			{
				CommonUtil.SetOwaTimeZone(mailboxSessionLock.Session, exTimeZone.Id);
				mailboxSessionLock.Session.ExTimeZone = exTimeZone;
			}
			base.WriteVariable("currentTimeZone", exTimeZone);
			return null;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000354B0 File Offset: 0x000336B0
		private void FirstTimeZone()
		{
			this.TimeZoneIndex = -1;
			this.NextTimeZone();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000354C0 File Offset: 0x000336C0
		private string NextTimeZone()
		{
			ExTimeZone exTimeZone = (ExTimeZone)this.ReadVariable("currentTimeZone");
			if (++this.TimeZoneIndex < this.timeZoneList.Count)
			{
				ExTimeZone exTimeZone2 = this.timeZoneList[this.TimeZoneIndex];
				base.WriteVariable("currentTimeZone", exTimeZone2);
				if (exTimeZone == null || exTimeZone.TimeZoneInformation.StandardBias != exTimeZone2.TimeZoneInformation.StandardBias)
				{
					TimeSpan standardBias = exTimeZone2.TimeZoneInformation.StandardBias;
					base.WriteVariable("offsetHours", Math.Abs(standardBias.Hours));
					base.WriteVariable("offsetMinutes", Math.Abs(standardBias.Minutes));
					base.WriteVariable("playGMTOffset", true);
					base.WriteVariable("positiveOffset", exTimeZone2.TimeZoneInformation.StandardBias.TotalMinutes >= 0.0);
				}
				else
				{
					base.WriteVariable("playGMTOffset", false);
				}
				return null;
			}
			base.WriteVariable("currentTimeZone", null);
			return "endOfTimeZoneList";
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000355F0 File Offset: 0x000337F0
		private string FindTimeZone()
		{
			string text = (string)this.ReadVariable("lastInput");
			if (string.IsNullOrEmpty(text))
			{
				return "invalidTimeFormat";
			}
			if (text.EndsWith("#", StringComparison.InvariantCulture))
			{
				text = text.Remove(text.Length - 1);
			}
			if (text.Length < 3 || text.Length > 4)
			{
				return "invalidTimeFormat";
			}
			TimeSpan targetLocalTime;
			try
			{
				int num = int.Parse(text.Substring(text.Length - 2, 2), CultureInfo.InvariantCulture);
				int num2 = int.Parse(text.Substring(0, text.Length - 2), CultureInfo.InvariantCulture);
				if (num > 59 || num2 > 23)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PersonalOptions::FindTimeZone: Either hours({0}) or minutes({1}) are out of range.", new object[]
					{
						num2,
						num
					});
					return "invalidTimeFormat";
				}
				targetLocalTime = new TimeSpan(num2, num, 0);
			}
			catch (FormatException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PersonalOptions::FindTimeZone: {0}.", new object[]
				{
					ex
				});
				return "invalidTimeFormat";
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "PersonalOptions::FindTimeZone: {0}.", new object[]
				{
					ex2
				});
				return "invalidTimeFormat";
			}
			this.timeZoneList = CommonUtil.GetTimeZonesForLocalTime(targetLocalTime, Constants.TimeZoneErrorSpan);
			if (this.timeZoneList.Count > 0)
			{
				this.FirstTimeZone();
			}
			if (this.timeZoneList.Count <= 0)
			{
				return "invalidTimeZone";
			}
			return null;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00035784 File Offset: 0x00033984
		private void ToggleASR(BaseUMCallSession vo)
		{
			base.LastRecoEvent = string.Empty;
			base.UseASR = !base.UseASR;
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			callerInfo.ConfigFolder.UseAsr = base.UseASR;
			callerInfo.ConfigFolder.Save();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000357D4 File Offset: 0x000339D4
		private void ToggleOOF(BaseUMCallSession vo)
		{
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			callerInfo.ConfigFolder.IsOof = !callerInfo.ConfigFolder.IsOof;
			callerInfo.ConfigFolder.Save();
			base.WriteVariable("Oof", callerInfo.ConfigFolder.IsOof);
			base.Manager.WriteVariable("Oof", callerInfo.ConfigFolder.IsOof);
			base.WriteVariable("lastAction", PersonalOptionsManager.LastAction.ToggleOOF.ToString());
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00035864 File Offset: 0x00033A64
		private void ToggleEmailOOF(BaseUMCallSession vo)
		{
			UMSubscriber callerInfo = vo.CurrentCallContext.CallerInfo;
			bool flag = !(bool)this.ReadVariable("emailOof");
			string replyText = Strings.DefaultEmailOOFText(callerInfo.DisplayName).ToString(callerInfo.TelephonyCulture);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = callerInfo.CreateSessionLock())
			{
				CommonUtil.SetEmailOOFStatus(mailboxSessionLock.Session, flag, replyText);
			}
			base.WriteVariable("emailOof", flag);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000358F0 File Offset: 0x00033AF0
		private void ToggleTimeFormat(BaseUMCallSession vo)
		{
			CultureInfo telephonyCulture = vo.CurrentCallContext.CallerInfo.TelephonyCulture;
			string text;
			string text2;
			CommonUtil.GetStandardTimeFormats(telephonyCulture, out text, out text2);
			string text3;
			if (CommonUtil.Is24HourTimeFormat(telephonyCulture.DateTimeFormat.ShortTimePattern))
			{
				text3 = text;
			}
			else
			{
				text3 = text2;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "ToggleTimeFormat -> Current:{0} - New:{1}.", new object[]
			{
				telephonyCulture.DateTimeFormat.ShortTimePattern,
				text3
			});
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = vo.CurrentCallContext.CallerInfo.CreateSessionLock())
			{
				CommonUtil.SetOwaTimeFormat(mailboxSessionLock.Session, text3);
			}
			telephonyCulture.DateTimeFormat.ShortTimePattern = text3;
			base.WriteVariable("timeFormat24", CommonUtil.Is24HourTimeFormat(telephonyCulture.DateTimeFormat.ShortTimePattern));
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000359C8 File Offset: 0x00033BC8
		private string GetNextFirstTimeUserTask()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "GetNextFirstTimeUserTask::FirstTimeUserContext={0}.", new object[]
			{
				this.firstTimeUserContext
			});
			if (this.firstTimeUserContext.PlayPassword)
			{
				this.firstTimeUserContext.PlayPassword = false;
				return "changePasswordTask";
			}
			if (this.firstTimeUserContext.PlayNameSetup)
			{
				this.firstTimeUserContext.PlayNameSetup = false;
				return "recordNameTask";
			}
			if (this.firstTimeUserContext.PlayExternalSetup)
			{
				this.firstTimeUserContext.PlayExternalSetup = false;
				return "recordExternalTask";
			}
			return null;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00035A54 File Offset: 0x00033C54
		private string ValidatePassword(UMSubscriber user, BaseUMCallSession vo)
		{
			string result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Validating password.  Password failure count is {0}.", new object[]
			{
				this.changePasswordFailures
			});
			UmPasswordManager umPasswordManager = new UmPasswordManager(user);
			if (umPasswordManager.IsValidPassword(base.Password))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Provided password is valid in ValidatePasswordAction.", new object[0]);
				this.firstPwd = base.Password;
				result = "passwordValidated";
			}
			else
			{
				this.changePasswordFailures++;
				this.firstPwd = null;
				base.Password = null;
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Password is invalid in ValidatePasswordAction. Failure Count={0}.", new object[]
				{
					this.changePasswordFailures
				});
				if (this.changePasswordFailures >= 5)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Disconnecting user after count={0} failed attempts in change password.", new object[]
					{
						this.changePasswordFailures
					});
					this.DropCall(vo, DropCallReason.UserError);
					result = "stopEvent";
				}
			}
			return result;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00035B50 File Offset: 0x00033D50
		private string MatchPasswords(UMSubscriber user, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Validating password.  Password failure count is {0}.", new object[]
			{
				this.changePasswordFailures
			});
			string result = null;
			if (base.Password == this.firstPwd)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Passwords match in MatchPasswords.  Setting user password.", new object[0]);
				this.changePasswordFailures = 0;
				UmPasswordManager umPasswordManager = new UmPasswordManager(user);
				umPasswordManager.SetPassword(base.Password);
				result = "passwordsMatch";
			}
			else
			{
				this.changePasswordFailures++;
				this.firstPwd = null;
				base.Password = null;
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Passwords do not match in MatchPasswordsAction. Failure Count={0}.", new object[]
				{
					this.changePasswordFailures
				});
				if (this.changePasswordFailures >= 5)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Disconnecting user after count={0} failed attempts in change password.", new object[]
					{
						this.changePasswordFailures
					});
					this.DropCall(vo, DropCallReason.UserError);
					result = "stopEvent";
				}
			}
			return result;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00035C58 File Offset: 0x00033E58
		private bool FetchGreeting(GreetingBase g)
		{
			base.WriteVariable("greeting", null);
			base.RecordContext.Reset();
			ITempWavFile tempWavFile = g.Get();
			if (tempWavFile == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "No greeting found for {0}.  Returning false.", new object[]
				{
					g.GetType()
				});
				return false;
			}
			base.WriteVariable("greeting", tempWavFile);
			return true;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00035CB8 File Offset: 0x00033EB8
		private void SaveGreeting(GreetingBase g)
		{
			ITempWavFile recording = base.RecordContext.Recording;
			base.RecordContext.Reset();
			if (recording != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Saving greeting at file={0} to store.", new object[]
				{
					recording.FilePath
				});
				g.Put(recording.FilePath);
			}
		}

		// Token: 0x04000A1F RID: 2591
		private EncryptedBuffer firstPwd;

		// Token: 0x04000A20 RID: 2592
		private int changePasswordFailures;

		// Token: 0x04000A21 RID: 2593
		private List<ExTimeZone> timeZoneList;

		// Token: 0x04000A22 RID: 2594
		private PersonalOptionsManager.SystemTaskContext systemTaskContext;

		// Token: 0x04000A23 RID: 2595
		private PersonalOptionsManager.FirstTimeUserContext firstTimeUserContext;

		// Token: 0x0200019F RID: 415
		internal enum LastAction
		{
			// Token: 0x04000A25 RID: 2597
			ToggleOOF,
			// Token: 0x04000A26 RID: 2598
			Greetings
		}

		// Token: 0x020001A0 RID: 416
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000C55 RID: 3157 RVA: 0x00035D0C File Offset: 0x00033F0C
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x00035D15 File Offset: 0x00033F15
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing PersonalOptions activity manager.", new object[0]);
				return new PersonalOptionsManager(manager, this);
			}
		}

		// Token: 0x020001A1 RID: 417
		private class SystemTaskContext
		{
			// Token: 0x06000C57 RID: 3159 RVA: 0x00035D34 File Offset: 0x00033F34
			internal SystemTaskContext(UMSubscriber user, ActivityManager manager)
			{
				UmPasswordManager umPasswordManager = new UmPasswordManager(user);
				this.firstTimeUserTask = user.ConfigFolder.IsFirstTimeUser;
				this.changePasswordTask = umPasswordManager.IsExpired;
				this.oofStatusTask = (manager != null && Shortcut.OOF == manager.LastShortcut);
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00035D80 File Offset: 0x00033F80
			internal bool IsFirstTimeUserTask
			{
				get
				{
					return this.firstTimeUserTask;
				}
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00035D88 File Offset: 0x00033F88
			internal bool IsChangePasswordTask
			{
				get
				{
					return this.changePasswordTask;
				}
			}

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00035D90 File Offset: 0x00033F90
			internal bool IsOofStatusTask
			{
				get
				{
					return this.oofStatusTask;
				}
			}

			// Token: 0x04000A27 RID: 2599
			private bool firstTimeUserTask;

			// Token: 0x04000A28 RID: 2600
			private bool changePasswordTask;

			// Token: 0x04000A29 RID: 2601
			private bool oofStatusTask;
		}

		// Token: 0x020001A2 RID: 418
		private class FirstTimeUserContext
		{
			// Token: 0x06000C5B RID: 3163 RVA: 0x00035D98 File Offset: 0x00033F98
			internal FirstTimeUserContext(UMSubscriber user)
			{
				GreetingBase greetingBase = null;
				try
				{
					UmPasswordManager umPasswordManager = new UmPasswordManager(user);
					this.playPassword = umPasswordManager.IsExpired;
					greetingBase = user.ConfigFolder.OpenNameGreeting();
					this.playNameSetup = !greetingBase.Exists();
					greetingBase.Dispose();
					greetingBase = user.ConfigFolder.OpenCustomMailboxGreeting(MailboxGreetingEnum.Voicemail);
					this.playExternalSetup = !greetingBase.Exists();
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "FirstTimeUserContext initialized {0}.", new object[]
					{
						this
					});
				}
				finally
				{
					if (greetingBase != null)
					{
						greetingBase.Dispose();
					}
				}
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00035E38 File Offset: 0x00034038
			// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00035E40 File Offset: 0x00034040
			internal bool PlayPassword
			{
				get
				{
					return this.playPassword;
				}
				set
				{
					this.playPassword = value;
				}
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00035E49 File Offset: 0x00034049
			// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00035E51 File Offset: 0x00034051
			internal bool PlayNameSetup
			{
				get
				{
					return this.playNameSetup;
				}
				set
				{
					this.playNameSetup = value;
				}
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00035E5A File Offset: 0x0003405A
			// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00035E62 File Offset: 0x00034062
			internal bool PlayExternalSetup
			{
				get
				{
					return this.playExternalSetup;
				}
				set
				{
					this.playExternalSetup = value;
				}
			}

			// Token: 0x06000C62 RID: 3170 RVA: 0x00035E6C File Offset: 0x0003406C
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "PlayPassword={0}, PlayNameSetup={1}, PlayExternalSetup={2}", new object[]
				{
					this.PlayPassword,
					this.PlayNameSetup,
					this.PlayExternalSetup
				});
			}

			// Token: 0x04000A2A RID: 2602
			private bool playPassword;

			// Token: 0x04000A2B RID: 2603
			private bool playNameSetup;

			// Token: 0x04000A2C RID: 2604
			private bool playExternalSetup;
		}
	}
}
