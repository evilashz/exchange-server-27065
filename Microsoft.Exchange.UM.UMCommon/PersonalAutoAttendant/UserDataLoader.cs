using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000120 RID: 288
	internal class UserDataLoader : DisposableBase, IDataLoader, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0002424C File Offset: 0x0002244C
		public UserDataLoader(UMSubscriber user, PhoneNumber callerId, string diversion)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			this.subscriber = user;
			this.callerId = callerId;
			this.diversion = diversion;
			this.cachedUserFreeBusy = FreeBusyStatusEnum.None;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00024280 File Offset: 0x00022480
		public PhoneNumber GetCallerId()
		{
			base.CheckDisposed();
			PIIMessage data = PIIMessage.Create(PIIType._Caller, this.callerId);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataLoader::GetCallerId() returning \"_Caller\"", new object[0]);
			return this.callerId;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000242C0 File Offset: 0x000224C0
		public string GetDiversionForCall()
		{
			base.CheckDisposed();
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::GetDiversionForCall() returning \"{0}\"", new object[]
			{
				this.diversion
			});
			return this.diversion;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000242FC File Offset: 0x000224FC
		public void GetUserOofSettings(out Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettings owaOof, out bool telOof)
		{
			base.CheckDisposed();
			if (!this.oofRead)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					this.owaOof = Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettings.GetUserOofSettings(mailboxSessionLock.Session);
					this.telOof = this.subscriber.ConfigFolder.IsOof;
					this.oofRead = true;
				}
			}
			owaOof = this.owaOof;
			telOof = this.telOof;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::GetUserOofSettings() returning UM=\"{0}\" OWA={1}", new object[]
			{
				telOof,
				owaOof
			});
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000243A4 File Offset: 0x000225A4
		public void GetFreeBusyInformation(out FreeBusyStatusEnum freeBusy)
		{
			base.CheckDisposed();
			if (this.freeBusyRead)
			{
				freeBusy = this.cachedUserFreeBusy;
				return;
			}
			freeBusy = FreeBusyStatusEnum.None;
			this.GetFreeBusyInformationFromAvailability(out freeBusy);
			this.cachedUserFreeBusy = freeBusy;
			this.freeBusyRead = true;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000243D8 File Offset: 0x000225D8
		public Microsoft.Exchange.InfoWorker.Common.Availability.WorkingHours GetWorkingHours()
		{
			base.CheckDisposed();
			if (!this.workingHoursRead)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					this.workingHours = Microsoft.Exchange.InfoWorker.Common.Availability.WorkingHours.LoadFrom(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar));
					this.workingHoursRead = true;
				}
			}
			if (ExTraceGlobals.PersonalAutoAttendantTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::GetWorkingHours() returning \"{0}\"", new object[]
				{
					(this.workingHours != null) ? this.workingHours.ToString() : "<null>"
				});
			}
			return this.workingHours;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00024488 File Offset: 0x00022688
		public PersonalContactInfo[] GetMatchingPersonalContacts(PhoneNumber callerId)
		{
			PIIMessage data = PIIMessage.Create(PIIType._Caller, callerId);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataLoader::GetMatchingPersonalContacts() callerid = \"_Caller\"", new object[0]);
			base.CheckDisposed();
			if (!this.calleridResolved)
			{
				LatencyDetectionContext latencyDetectionContext = PAAUtils.ResolvePersonalContactsFactory.CreateContext(CommonConstants.ApplicationVersion, CallId.Id ?? string.Empty, new IPerformanceDataProvider[]
				{
					RpcDataProvider.Instance,
					PerformanceContext.Current
				});
				List<PersonalContactInfo> list = PersonalContactInfo.FindAllMatchingContacts(this.subscriber, this.callerId);
				TaskPerformanceData[] array = latencyDetectionContext.StopAndFinalizeCollection();
				TaskPerformanceData taskPerformanceData = array[0];
				PerformanceData end = taskPerformanceData.End;
				if (end != PerformanceData.Zero)
				{
					PerformanceData difference = taskPerformanceData.Difference;
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::GetMatchingPersonalContacts() [RPCRequests = {0} RPCLatency = {1}", new object[]
					{
						difference.Count,
						difference.Milliseconds
					});
				}
				this.matchingContacts = list.ToArray();
				this.calleridResolved = true;
			}
			return this.matchingContacts;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00024590 File Offset: 0x00022790
		public ADContactInfo GetMatchingADContact(PhoneNumber callerId)
		{
			PIIMessage data = PIIMessage.Create(PIIType._Caller, callerId);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataLoader::GetMatchingADContact() callerid = \"_Caller\"", new object[0]);
			base.CheckDisposed();
			if (!this.directoryContactResolved)
			{
				this.matchingADContact = ADContactInfo.FindCallerByCallerId(this.subscriber, callerId);
				this.directoryContactResolved = true;
			}
			return this.matchingADContact;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000245EC File Offset: 0x000227EC
		public List<string> GetMatchingPersonaEmails()
		{
			if (this.matchingPersonaEmails != null)
			{
				return this.matchingPersonaEmails;
			}
			List<string> list = new List<string>();
			if (this.matchingADContact != null)
			{
				PersonaType personaType = null;
				using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(this.subscriber.ADUser, false))
				{
					personaType = umuserMailboxAccessor.GetPersonaFromEmail(this.matchingADContact.EMailAddress);
				}
				if (personaType != null)
				{
					PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.matchingADContact.EMailAddress);
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "Got matching Persona for matching AD contact _EmailAddress", new object[0]);
					foreach (EmailAddressType emailAddressType in personaType.EmailAddresses)
					{
						if (emailAddressType != null)
						{
							list.Add(emailAddressType.EmailAddress.ToLower());
						}
					}
				}
			}
			if (this.matchingContacts != null)
			{
				PersonalContactInfo[] array = this.matchingContacts;
				int j = 0;
				while (j < array.Length)
				{
					PersonalContactInfo personalContactInfo = array[j];
					if (personalContactInfo.PersonId == null)
					{
						goto IL_1A4;
					}
					Person person = null;
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
					{
						person = Person.LoadWithGALAggregation(mailboxSessionLock.Session, personalContactInfo.PersonId, UserDataLoader.LoadPersonColumns, null);
					}
					if (person != null && person.EmailAddresses != null)
					{
						using (IEnumerator<Participant> enumerator = person.EmailAddresses.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Participant participant = enumerator.Current;
								if (participant.EmailAddress != null)
								{
									PIIMessage data2 = PIIMessage.Create(PIIType._EmailAddress, participant.EmailAddress);
									CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data2, "Adding Email address for matching Personal Contacts _EmailAddress", new object[0]);
									list.Add(participant.EmailAddress.ToLower());
								}
							}
							goto IL_1B6;
						}
						goto IL_1A4;
					}
					IL_1B6:
					j++;
					continue;
					IL_1A4:
					list.Add(personalContactInfo.EMailAddress.ToLower());
					goto IL_1B6;
				}
			}
			this.matchingPersonaEmails = list;
			return this.matchingPersonaEmails;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000247F8 File Offset: 0x000229F8
		public ExTimeZone GetUserTimeZone()
		{
			base.CheckDisposed();
			if (this.owaTimeZone == null)
			{
				this.owaTimeZone = this.subscriber.TimeZone;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::GetUserTimeZone() returning \"{0}\"", new object[]
			{
				this.owaTimeZone.DisplayName
			});
			return this.owaTimeZone;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00024850 File Offset: 0x00022A50
		public UMSubscriber GetUMSubscriber()
		{
			base.CheckDisposed();
			PIIMessage data = PIIMessage.Create(PIIType._Caller, this.callerId);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataLoader::GetUMSubscriber() returning \"_Caller\"", new object[0]);
			return this.subscriber;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00024890 File Offset: 0x00022A90
		public bool TryIsWithinCompanyWorkingHours(out bool withinWorkingHours)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataLoader::TryIsWithinCompanyWorkingHours()", new object[0]);
			base.CheckDisposed();
			withinWorkingHours = false;
			if (this.subscriber.DialPlan.UMAutoAttendant == null)
			{
				return false;
			}
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.subscriber.ADRecipient);
			UMAutoAttendant autoAttendantFromId = iadsystemConfigurationLookup.GetAutoAttendantFromId(this.subscriber.DialPlan.UMAutoAttendant);
			if (autoAttendantFromId == null)
			{
				return false;
			}
			HolidaySchedule holidaySchedule = null;
			autoAttendantFromId.GetCurrentSettings(out holidaySchedule, ref withinWorkingHours);
			return true;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002490A File Offset: 0x00022B0A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.subscriber = null;
			}
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024916 File Offset: 0x00022B16
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserDataLoader>(this);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00024920 File Offset: 0x00022B20
		private void GetFreeBusyInformationFromAvailability(out FreeBusyStatusEnum freeBusy)
		{
			base.CheckDisposed();
			freeBusy = FreeBusyStatusEnum.None;
			ExDateTime now = ExDateTime.GetNow(this.GetUserTimeZone());
			int minute = now.Minute / 5 * 5;
			ExDateTime exDateTime = new ExDateTime(now.TimeZone, now.Year, now.Month, now.Day, now.Hour, minute, 0);
			ExDateTime exDateTime2 = exDateTime.AddMinutes(5.0);
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "UserDataLoader:GetFreeBusyInformation() Now='{0}' Calendar Window = '{1}'-'{2}'", new object[]
			{
				now,
				exDateTime,
				exDateTime2
			});
			LatencyDetectionContext latencyDetectionContext = PAAUtils.GetFreeBusyInfoFactory.CreateContext(CommonConstants.ApplicationVersion, CallId.Id ?? string.Empty, new IPerformanceDataProvider[]
			{
				RpcDataProvider.Instance,
				PerformanceContext.Current
			});
			string text = null;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSessionLock.Session, DefaultFolderType.Calendar))
				{
					text = MergedFreeBusy.GetMergedFreeBusyString(new Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress(this.subscriber.DisplayName, this.subscriber.MailAddress), calendarFolder, this.GetUserTimeZone(), exDateTime, exDateTime2);
				}
			}
			TaskPerformanceData[] array = latencyDetectionContext.StopAndFinalizeCollection();
			TaskPerformanceData taskPerformanceData = array[0];
			PerformanceData end = taskPerformanceData.End;
			if (end != PerformanceData.Zero)
			{
				PerformanceData difference = taskPerformanceData.Difference;
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.subscriber.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataLoader:GetFreeBusyInformation() User=_EmailAddress RPCRequests = {0}, RPCLatency = {1}", new object[]
				{
					difference.Count,
					difference.Milliseconds
				});
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "UserDataLoader:GetFreeBusyInformation() Merged F/B='{0}'", new object[]
			{
				text
			});
			if (text.Length > 0)
			{
				string text2 = text.Substring(0, 1);
				switch (int.Parse(text2, CultureInfo.InvariantCulture))
				{
				case 0:
					freeBusy = FreeBusyStatusEnum.Free;
					goto IL_273;
				case 1:
					freeBusy = FreeBusyStatusEnum.Tentative;
					goto IL_273;
				case 2:
					freeBusy = FreeBusyStatusEnum.Busy;
					goto IL_273;
				case 3:
					freeBusy = FreeBusyStatusEnum.OutOfOffice;
					goto IL_273;
				case 5:
					freeBusy = FreeBusyStatusEnum.NotAvailable;
					goto IL_273;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid BusyType '{0}' in merged freebusy string", new object[]
				{
					text2
				}));
				IL_273:
				CallIdTracer.TraceDebug(ExTraceGlobals.AutoAttendantTracer, this, "UserDataLoader:GetFreeBusyInformation() Returning F/B = '{0}'", new object[]
				{
					freeBusy
				});
				return;
			}
			throw new InvalidOperationException("Got an empty Merged FreeBusy string");
		}

		// Token: 0x04000525 RID: 1317
		private static PropertyDefinition[] LoadPersonColumns = new PropertyDefinition[]
		{
			PersonSchema.EmailAddresses
		};

		// Token: 0x04000526 RID: 1318
		private UMSubscriber subscriber;

		// Token: 0x04000527 RID: 1319
		private PhoneNumber callerId;

		// Token: 0x04000528 RID: 1320
		private string diversion;

		// Token: 0x04000529 RID: 1321
		private Microsoft.Exchange.InfoWorker.Common.OOF.UserOofSettings owaOof;

		// Token: 0x0400052A RID: 1322
		private bool telOof;

		// Token: 0x0400052B RID: 1323
		private bool oofRead;

		// Token: 0x0400052C RID: 1324
		private Microsoft.Exchange.InfoWorker.Common.Availability.WorkingHours workingHours;

		// Token: 0x0400052D RID: 1325
		private bool workingHoursRead;

		// Token: 0x0400052E RID: 1326
		private PersonalContactInfo[] matchingContacts;

		// Token: 0x0400052F RID: 1327
		private List<string> matchingPersonaEmails;

		// Token: 0x04000530 RID: 1328
		private bool calleridResolved;

		// Token: 0x04000531 RID: 1329
		private ADContactInfo matchingADContact;

		// Token: 0x04000532 RID: 1330
		private bool directoryContactResolved;

		// Token: 0x04000533 RID: 1331
		private ExTimeZone owaTimeZone;

		// Token: 0x04000534 RID: 1332
		private bool freeBusyRead;

		// Token: 0x04000535 RID: 1333
		private FreeBusyStatusEnum cachedUserFreeBusy;
	}
}
