using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000108 RID: 264
	internal class PAAStore : DisposableBase, IPAAStore, IDisposeTrackable, IDisposable
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x000205BC File Offset: 0x0001E7BC
		private PAAStore(UMSubscriber u)
		{
			u.AddReference();
			this.Initialize(u);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000205DC File Offset: 0x0001E7DC
		private PAAStore(ExchangePrincipal principal)
		{
			try
			{
				if (principal == null)
				{
					throw new ArgumentNullException("principal");
				}
				UMSubscriber umsubscriber = UMRecipient.Factory.FromPrincipal<UMSubscriber>(principal);
				if (umsubscriber == null)
				{
					throw new InvalidPrincipalException();
				}
				this.Initialize(umsubscriber);
			}
			catch (LocalizedException ex)
			{
				this.DebugTrace("{0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002064C File Offset: 0x0001E84C
		public static IPAAStore Create(UMSubscriber u)
		{
			return new PAAStore(u);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00020654 File Offset: 0x0001E854
		public static IPAAStore Create(ExchangePrincipal principal)
		{
			return new PAAStore(principal);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002065C File Offset: 0x0001E85C
		public IList<PersonalAutoAttendant> GetAutoAttendants()
		{
			base.CheckDisposed();
			PAAStoreStatus paastoreStatus;
			return this.GetAutoAttendants(PAAValidationMode.None, out paastoreStatus);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00020678 File Offset: 0x0001E878
		public IList<PersonalAutoAttendant> GetAutoAttendants(PAAValidationMode validationMode)
		{
			if (validationMode == PAAValidationMode.StopOnFirstError)
			{
				throw new LocalizedException(new LocalizedString("Some exception"));
			}
			PAAStoreStatus paastoreStatus;
			return this.GetAutoAttendants(validationMode, out paastoreStatus);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000206A4 File Offset: 0x0001E8A4
		public bool TryGetAutoAttendants(PAAValidationMode validationMode, out IList<PersonalAutoAttendant> autoAttendants)
		{
			PAAStoreStatus paastoreStatus;
			autoAttendants = this.GetAutoAttendantsFromStore(validationMode, out paastoreStatus, true);
			return autoAttendants != null && autoAttendants.Count > 0;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000206CD File Offset: 0x0001E8CD
		public IList<PersonalAutoAttendant> GetAutoAttendants(PAAValidationMode validationMode, out PAAStoreStatus storeStatus)
		{
			return this.GetAutoAttendantsFromStore(validationMode, out storeStatus, false);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public void Save(IList<PersonalAutoAttendant> autoattendants)
		{
			base.CheckDisposed();
			if (autoattendants == null)
			{
				throw new ArgumentNullException("autoattendants");
			}
			if (autoattendants.Count > 9)
			{
				throw new MaxPAACountReachedException(9);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::Save() #autoattendants={0}", new object[]
			{
				autoattendants.Count
			});
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
			{
				using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
				{
					using (Stream stream = config.GetStream())
					{
						stream.SetLength(0L);
						PAAParser.Instance.Serialize(autoattendants, stream);
						config.Save();
					}
				}
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000207B8 File Offset: 0x0001E9B8
		public PersonalAutoAttendant GetAutoAttendant(Guid identity, PAAValidationMode validationMode)
		{
			base.CheckDisposed();
			IList<PersonalAutoAttendant> autoAttendants = this.GetAutoAttendants();
			int num = -1;
			PersonalAutoAttendant personalAutoAttendant = this.FindAutoAttendantByGuid(autoAttendants, identity, out num);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendant(id= {0},validationMode = {1}) returning autoattendant={2}", new object[]
			{
				identity,
				validationMode,
				(personalAutoAttendant != null) ? personalAutoAttendant.Name : "<null>"
			});
			if (personalAutoAttendant != null && validationMode != PAAValidationMode.None)
			{
				this.Validate(personalAutoAttendant, validationMode);
			}
			return personalAutoAttendant;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00020830 File Offset: 0x0001EA30
		public bool TryGetAutoAttendant(Guid identity, PAAValidationMode validationMode, out PersonalAutoAttendant autoAttendant)
		{
			base.CheckDisposed();
			autoAttendant = null;
			IList<PersonalAutoAttendant> autoattendants = null;
			if (!this.TryGetAutoAttendants(validationMode, out autoattendants))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::TryGetAutoAttendant(id= {0},validationMode = {1}) Did not find autoattendants", new object[]
				{
					identity,
					validationMode
				});
				return false;
			}
			int num = -1;
			autoAttendant = this.FindAutoAttendantByGuid(autoattendants, identity, out num);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::TryGetAutoAttendant(id= {0},validationMode = {1}) returning autoattendant={2}", new object[]
			{
				identity,
				validationMode,
				(autoAttendant != null) ? autoAttendant.Name : "<null>"
			});
			if (autoAttendant != null && validationMode != PAAValidationMode.None)
			{
				this.Validate(autoAttendant, validationMode);
			}
			return autoAttendant != null;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public void DeleteAutoAttendant(Guid identity)
		{
			base.CheckDisposed();
			IList<PersonalAutoAttendant> autoAttendants = this.GetAutoAttendants();
			int num = -1;
			PersonalAutoAttendant paa = this.FindAutoAttendantByGuid(autoAttendants, identity, out num);
			if (num == -1)
			{
				throw new ObjectNotFoundException(Strings.PersonalAutoAttendantNotFound(identity.ToString()));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::DeleteAutoAttendant({0}) deleting autoattendant at index = {1}", new object[]
			{
				identity,
				num
			});
			autoAttendants.RemoveAt(num);
			this.Save(autoAttendants);
			this.DeleteGreeting(paa);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002096C File Offset: 0x0001EB6C
		public void DeletePAAConfiguration()
		{
			base.CheckDisposed();
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::Delete()", new object[0]);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
			{
				mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					"UM.E14.PersonalAutoAttendants"
				});
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000209E0 File Offset: 0x0001EBE0
		public GreetingBase OpenGreeting(PersonalAutoAttendant paa)
		{
			base.CheckDisposed();
			if (paa == null)
			{
				throw new ArgumentNullException("paa");
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::OpenGreeting({0}) opening autoattendant greeting for autoattendant = {1}", new object[]
			{
				paa.Name,
				paa.Identity
			});
			object[] args = new object[]
			{
				this.subscriber,
				paa.Greeting
			};
			return (GreetingBase)Activator.CreateInstance(XsoConfigurationFolder.CoreTypeLoader.XsoGreetingType, BindingFlags.Instance | BindingFlags.NonPublic, null, args, null);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00020A60 File Offset: 0x0001EC60
		public void GetUserPermissions(out bool enabledForPersonalAutoAttendant, out bool enabledForOutdialing)
		{
			base.CheckDisposed();
			enabledForPersonalAutoAttendant = this.subscriber.IsPAAEnabled;
			enabledForOutdialing = this.subscriber.IsEnabledForOutcalling;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetUserPermissions() returns paaEnabled={0} outdialingEnabled={1}", new object[]
			{
				enabledForPersonalAutoAttendant,
				enabledForOutdialing
			});
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public bool Validate(PersonalAutoAttendant paa, PAAValidationMode validationMode)
		{
			base.CheckDisposed();
			if (paa == null)
			{
				throw new ArgumentNullException("paa");
			}
			bool flag = false;
			if (validationMode != PAAValidationMode.None)
			{
				flag = paa.Validate(this.dataValidator, validationMode);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::Validate(ID={0},Enabled={1} Mode={2}) returns {3}", new object[]
			{
				paa.Identity,
				paa.Enabled,
				validationMode,
				flag
			});
			return flag;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00020B37 File Offset: 0x0001ED37
		public IList<string> GetExtensionsInPrimaryDialPlan()
		{
			base.CheckDisposed();
			return Utils.GetExtensionsInDialPlanValidForPAA(this.subscriber.DialPlan, this.subscriber.ADRecipient);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00020B5A File Offset: 0x0001ED5A
		public bool ValidatePhoneNumberForOutdialing(string number, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidatePhoneNumberForOutdialing(number, out result);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00020B6F File Offset: 0x0001ED6F
		public bool ValidateADContactForOutdialing(string legacyExchangeDN, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidateADContactForOutdialing(legacyExchangeDN, out result);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00020B84 File Offset: 0x0001ED84
		public bool ValidateADContactForTransferToMailbox(string legacyExchangeDN, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidateADContactForTransferToMailbox(legacyExchangeDN, out result);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00020B99 File Offset: 0x0001ED99
		public bool ValidateContactItemCallerId(StoreObjectId storeId, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidateContactItemCallerId(storeId, out result);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00020BAE File Offset: 0x0001EDAE
		public bool ValidateADContactCallerId(string exchangeLegacyDN, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidateADContactCallerId(exchangeLegacyDN, out result);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00020BC3 File Offset: 0x0001EDC3
		public bool ValidatePhoneNumberCallerId(string number, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidatePhoneNumberCallerId(number, out result);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00020BD8 File Offset: 0x0001EDD8
		public bool ValidateContactFolderCallerId(out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidateContactFolderCallerId(out result);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00020BEC File Offset: 0x0001EDEC
		public bool ValidatePersonaContactCallerId(string emailAddress, out IDataValidationResult result)
		{
			base.CheckDisposed();
			return this.dataValidator.ValidatePersonaContactCallerId(emailAddress, out result);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00020C04 File Offset: 0x0001EE04
		public void DeleteGreeting(PersonalAutoAttendant paa)
		{
			if (paa != null && paa.Greeting != null)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
					{
						"Um.CustomGreetings." + paa.Greeting
					});
				}
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00020C70 File Offset: 0x0001EE70
		public bool ValidateExtensions(IList<string> extensions, out PAAValidationResult result, out string extensionInError)
		{
			base.CheckDisposed();
			ValidateArgument.NotNull(extensions, "extensions");
			return this.dataValidator.ValidateExtensions(extensions, out result, out extensionInError);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00020C94 File Offset: 0x0001EE94
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dataValidator != null)
				{
					this.dataValidator.Dispose();
				}
				this.subscriber.ReleaseReference();
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::Dispose() disposed = {0}", new object[]
				{
					base.IsDisposed
				});
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PAAStore>(this);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		private PersonalAutoAttendant FindAutoAttendantByGuid(IList<PersonalAutoAttendant> autoattendants, Guid identity, out int index)
		{
			index = -1;
			PersonalAutoAttendant result = null;
			if (autoattendants != null)
			{
				for (int i = 0; i < autoattendants.Count; i++)
				{
					if (autoattendants[i].Identity == identity)
					{
						index = i;
						result = autoattendants[i];
						break;
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendantByGuid({0}) found autoattendant at index = {1}", new object[]
			{
				identity,
				index
			});
			return result;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00020D63 File Offset: 0x0001EF63
		private void DebugTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this.GetHashCode(), this.tracePrefix + formatString, formatObjects);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00020D88 File Offset: 0x0001EF88
		private void Initialize(UMSubscriber user)
		{
			this.subscriber = user;
			this.dataValidator = new UserDataValidator(user);
			this.tracePrefix = string.Format(CultureInfo.InvariantCulture, "{0}({1}): ", new object[]
			{
				base.GetType().Name,
				this.subscriber.ExchangePrincipal.MailboxInfo.DisplayName
			});
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00020DEC File Offset: 0x0001EFEC
		private UserConfiguration GetConfig(MailboxSession session)
		{
			base.CheckDisposed();
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = session.UserConfigurationManager.GetMailboxConfiguration("UM.E14.PersonalAutoAttendants", UserConfigurationTypes.Stream);
			}
			catch (ObjectNotFoundException)
			{
				PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.subscriber.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, data, "Creating UM General configuration folder for user: _EmailAddress.", new object[0]);
				userConfiguration = session.UserConfigurationManager.CreateMailboxConfiguration("UM.E14.PersonalAutoAttendants", UserConfigurationTypes.Stream);
			}
			catch (CorruptDataException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.XsoTracer, this, "Exception : {0}", new object[]
				{
					ex
				});
				throw;
			}
			catch (InvalidOperationException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.XsoTracer, this, "Exception : {0}", new object[]
				{
					ex2
				});
				throw;
			}
			if (userConfiguration == null)
			{
				PIIMessage data2 = PIIMessage.Create(PIIType._UserDisplayName, this.subscriber.ADRecipient.DisplayName);
				CallIdTracer.TraceError(ExTraceGlobals.XsoTracer, this, data2, "get_GeneralConfiguration() returning NULL configuration for user: _DisplayName", new object[0]);
				throw new InvalidOperationException("Could not bind to PAA store");
			}
			return userConfiguration;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00020F04 File Offset: 0x0001F104
		private IList<PersonalAutoAttendant> GetAutoAttendantsFromStore(PAAValidationMode validationMode, out PAAStoreStatus storeStatus, bool suppressExceptions)
		{
			base.CheckDisposed();
			storeStatus = PAAStoreStatus.None;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() validate={0}", new object[]
			{
				validationMode
			});
			LatencyDetectionContext latencyDetectionContext = null;
			IList<PersonalAutoAttendant> list = new PAAStore.PAAList();
			if (this.subscriber.RequiresRedirectForCallAnswering())
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() subscriber RequiresRedirectForCallAnswering", new object[0]);
				return list;
			}
			try
			{
				latencyDetectionContext = PAAUtils.GetAutoAttendantsFromStoreFactory.CreateContext(CommonConstants.ApplicationVersion, CallId.Id ?? string.Empty, new IPerformanceDataProvider[]
				{
					RpcDataProvider.Instance,
					PerformanceContext.Current
				});
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
					{
						using (Stream stream = config.GetStream())
						{
							if (stream.Length > 0L)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() commencing parsing stream from mailbox [bytes = {0}]", new object[]
								{
									stream.Length
								});
								PAAParser.Instance.Parse(list, stream);
								storeStatus = PAAStoreStatus.Valid;
								CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() finished parsing stream from mailbox", new object[0]);
							}
						}
					}
				}
			}
			catch (XmlException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() Exception = {0}", new object[]
				{
					ex
				});
				storeStatus = PAAStoreStatus.Corrupted;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CorruptedPAAStore, this.subscriber.MailAddress, new object[]
				{
					this.subscriber.MailAddress
				});
				if (!suppressExceptions)
				{
					throw;
				}
			}
			catch (CorruptedPAAStoreException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() Exception = {0}", new object[]
				{
					ex2
				});
				storeStatus = PAAStoreStatus.Corrupted;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CorruptedPAAStore, this.subscriber.MailAddress, new object[]
				{
					this.subscriber.MailAddress
				});
				if (!suppressExceptions)
				{
					throw;
				}
			}
			catch (LocalizedException ex3)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() Exception = {0}", new object[]
				{
					ex3
				});
				if (!suppressExceptions)
				{
					throw;
				}
				return list;
			}
			finally
			{
				TaskPerformanceData[] array = latencyDetectionContext.StopAndFinalizeCollection();
				TaskPerformanceData taskPerformanceData = array[0];
				PerformanceData end = taskPerformanceData.End;
				if (end != PerformanceData.Zero)
				{
					PerformanceData difference = taskPerformanceData.Difference;
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() GetAutoAttendants from Config [RPCRequests = {0} RPCLatency = {1}", new object[]
					{
						difference.Count,
						difference.Milliseconds
					});
				}
			}
			if (storeStatus == PAAStoreStatus.Corrupted)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CorruptedPAAStore, this.subscriber.MailAddress, new object[]
				{
					this.subscriber.MailAddress
				});
				return list;
			}
			if (validationMode != PAAValidationMode.None)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (!list[i].IsCompatible)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() AutoAttendant with ID {0} is Incompatible. Skipping validation", new object[]
						{
							list[i].Identity
						});
					}
					else
					{
						this.Validate(list[i], validationMode);
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAStore::GetAutoAttendants() returning #autoattendants={0} StoreStatus = {1}", new object[]
			{
				list.Count,
				storeStatus
			});
			return list;
		}

		// Token: 0x040004EC RID: 1260
		private const int MaxPAACount = 9;

		// Token: 0x040004ED RID: 1261
		private UMSubscriber subscriber;

		// Token: 0x040004EE RID: 1262
		private string tracePrefix = string.Empty;

		// Token: 0x040004EF RID: 1263
		private IDataValidator dataValidator;

		// Token: 0x02000109 RID: 265
		internal class PAAList : IList<PersonalAutoAttendant>, ICollection<PersonalAutoAttendant>, IEnumerable<PersonalAutoAttendant>, IEnumerable
		{
			// Token: 0x060008C6 RID: 2246 RVA: 0x00021334 File Offset: 0x0001F534
			internal PAAList()
			{
				this.autoattendants = new List<PersonalAutoAttendant>();
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00021347 File Offset: 0x0001F547
			public int Count
			{
				get
				{
					return this.autoattendants.Count;
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00021354 File Offset: 0x0001F554
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700021B RID: 539
			public PersonalAutoAttendant this[int index]
			{
				get
				{
					return this.autoattendants[index];
				}
				set
				{
					this.autoattendants[index] = value;
				}
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00021374 File Offset: 0x0001F574
			public int IndexOf(PersonalAutoAttendant item)
			{
				return this.autoattendants.IndexOf(item);
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x00021382 File Offset: 0x0001F582
			public void Insert(int index, PersonalAutoAttendant item)
			{
				this.autoattendants.Insert(index, item);
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x00021391 File Offset: 0x0001F591
			public void RemoveAt(int index)
			{
				this.autoattendants.RemoveAt(index);
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x0002139F File Offset: 0x0001F59F
			public void Add(PersonalAutoAttendant item)
			{
				this.autoattendants.Add(item);
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x000213AD File Offset: 0x0001F5AD
			public void Clear()
			{
				this.autoattendants.Clear();
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x000213BA File Offset: 0x0001F5BA
			public bool Contains(PersonalAutoAttendant item)
			{
				return this.autoattendants.Contains(item);
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x000213C8 File Offset: 0x0001F5C8
			public void CopyTo(PersonalAutoAttendant[] array, int arrayIndex)
			{
				this.autoattendants.CopyTo(array, arrayIndex);
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x000213D7 File Offset: 0x0001F5D7
			public bool Remove(PersonalAutoAttendant item)
			{
				return this.autoattendants.Remove(item);
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x000213E5 File Offset: 0x0001F5E5
			public IEnumerator<PersonalAutoAttendant> GetEnumerator()
			{
				return this.autoattendants.GetEnumerator();
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x000213F7 File Offset: 0x0001F5F7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.autoattendants.GetEnumerator();
			}

			// Token: 0x040004F0 RID: 1264
			private List<PersonalAutoAttendant> autoattendants;
		}
	}
}
