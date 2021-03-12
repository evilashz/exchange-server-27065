using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000DE RID: 222
	internal class MeetingOrganizerInfo
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00043F4C File Offset: 0x0004214C
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00043F53 File Offset: 0x00042153
		internal static Func<GlobalObjectId, MeetingOrganizerEntry> OnCacheMissForTest { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00043F5B File Offset: 0x0004215B
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00043F63 File Offset: 0x00042163
		public ExDateTime LastCleanTime { get; private set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00043F6C File Offset: 0x0004216C
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00043F74 File Offset: 0x00042174
		public bool IsDirty { get; private set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00043F80 File Offset: 0x00042180
		public int Count
		{
			get
			{
				int count;
				lock (this.instanceLock)
				{
					count = this.map.Count;
				}
				return count;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00043FC8 File Offset: 0x000421C8
		public MeetingOrganizerInfo()
		{
			this.LastCleanTime = (ExDateTime)TimeProvider.UtcNow;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00043FF8 File Offset: 0x000421F8
		public void Add(CalendarItemBase calendarItem)
		{
			GlobalObjectId globalObjectId = null;
			if (calendarItem == null)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] calendarItem is null.");
				return;
			}
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] Process CalendarItem and add an entry to Cache");
			string text = string.Empty;
			try
			{
				text = calendarItem.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			}
			catch (NullReferenceException)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] nullReference Exception when reading PrimarySmtpAddress");
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] PrimarySmtpAddress:{0}", text);
			try
			{
				globalObjectId = this.MakeCleanId(calendarItem.GlobalObjectId);
			}
			catch (CorruptDataException arg)
			{
				AirSyncDiagnostics.TraceDebug<string, string, CorruptDataException>(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] calendarItem does NOT have a global object id.  Subject: '{0}', Mailbox: '{1}', Exception: '{2}'", calendarItem.Subject, text, arg);
				return;
			}
			catch (NotInBagPropertyErrorException arg2)
			{
				AirSyncDiagnostics.TraceDebug<string, string, NotInBagPropertyErrorException>(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] calendarItem does NOT have a global object id.  Subject: '{0}', Mailbox: '{1}', Exception: '{2}'", calendarItem.Subject, text, arg2);
				return;
			}
			if (globalObjectId != null && globalObjectId.Uid != null)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] Found CleanId & Uid:{0}", globalObjectId.Uid);
				MeetingOrganizerEntry meetingOrganizerEntry = null;
				bool flag = false;
				lock (this.instanceLock)
				{
					this.map.TryGetValue(globalObjectId.Uid, out meetingOrganizerEntry);
				}
				if (meetingOrganizerEntry == null)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] Entry not found in Dictionary");
					meetingOrganizerEntry = ((MeetingOrganizerInfo.OnCacheMissForTest != null) ? MeetingOrganizerInfo.OnCacheMissForTest(globalObjectId) : this.BuildFromCalendarItem(globalObjectId, calendarItem));
					if (meetingOrganizerEntry != null)
					{
						if (meetingOrganizerEntry.CleanGlobalObjectId == null || meetingOrganizerEntry.CleanGlobalObjectId.Uid == null)
						{
							Command.CurrentCommand.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, "Add_NullUID");
						}
						else if (meetingOrganizerEntry.IsOrganizer != null)
						{
							this.Add(meetingOrganizerEntry);
						}
					}
				}
				else
				{
					flag = true;
				}
				if (Command.CurrentCommand != null)
				{
					Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.MeetingOrganizerLookup, flag.ToString());
				}
				return;
			}
			AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "[MeetingOrganizerInfo.Add] cleanId is missing Uid. Tell someone to go find it.  Subject: '{0}', Mailbox: '{1}'", calendarItem.Subject, text);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00044210 File Offset: 0x00042410
		public MeetingOrganizerEntry GetEntry(GlobalObjectId globalObjectId)
		{
			if (globalObjectId == null)
			{
				return null;
			}
			GlobalObjectId globalObjectId2 = this.MakeCleanId(globalObjectId);
			MeetingOrganizerEntry meetingOrganizerEntry = null;
			bool flag = false;
			lock (this.instanceLock)
			{
				this.map.TryGetValue(globalObjectId2.Uid, out meetingOrganizerEntry);
			}
			if (meetingOrganizerEntry == null)
			{
				meetingOrganizerEntry = ((MeetingOrganizerInfo.OnCacheMissForTest != null) ? MeetingOrganizerInfo.OnCacheMissForTest(globalObjectId) : this.ReadFromStore(globalObjectId));
				if (meetingOrganizerEntry != null)
				{
					if (meetingOrganizerEntry.CleanGlobalObjectId.Uid == null)
					{
						Command.CurrentCommand.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, "Get_NullUID");
					}
					else if (meetingOrganizerEntry.IsOrganizer != null)
					{
						this.Add(meetingOrganizerEntry);
					}
				}
			}
			else
			{
				flag = true;
			}
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.MeetingOrganizerLookup, flag.ToString());
			}
			return meetingOrganizerEntry;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000442F8 File Offset: 0x000424F8
		public bool Contains(GlobalObjectId globalObjectId)
		{
			GlobalObjectId globalObjectId2 = this.MakeCleanId(globalObjectId);
			bool result;
			lock (this.instanceLock)
			{
				result = this.map.ContainsKey(globalObjectId2.Uid);
			}
			return result;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00044350 File Offset: 0x00042550
		public bool Add(MeetingOrganizerEntry entry)
		{
			this.CleanIfNecessary();
			if (entry.CleanGlobalObjectId == null || entry.CleanGlobalObjectId.Uid == null)
			{
				return false;
			}
			bool result;
			lock (this.instanceLock)
			{
				if (this.map.ContainsKey(entry.CleanGlobalObjectId.Uid))
				{
					result = false;
				}
				else
				{
					this.map[entry.CleanGlobalObjectId.Uid] = entry;
					this.IsDirty = true;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x000443E4 File Offset: 0x000425E4
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x000443EC File Offset: 0x000425EC
		internal bool IgnoreCleanupTimeIntervalCheckForTest { get; set; }

		// Token: 0x06000CBC RID: 3260 RVA: 0x000443F5 File Offset: 0x000425F5
		internal Dictionary<string, MeetingOrganizerEntry> GetMapForTest()
		{
			return this.map;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00044400 File Offset: 0x00042600
		internal void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool)
		{
			lock (this.instanceLock)
			{
				componentDataPool.GetDateTimeDataInstance().Bind(this.LastCleanTime).SerializeData(writer, componentDataPool);
				GenericDictionaryData<StringData, string, MeetingOrganizerEntryData, MeetingOrganizerEntry> genericDictionaryData = new GenericDictionaryData<StringData, string, MeetingOrganizerEntryData, MeetingOrganizerEntry>(this.map);
				genericDictionaryData.SerializeData(writer, componentDataPool);
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00044468 File Offset: 0x00042668
		internal void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			lock (this.instanceLock)
			{
				DateTimeData dateTimeDataInstance = componentDataPool.GetDateTimeDataInstance();
				dateTimeDataInstance.DeserializeData(reader, componentDataPool);
				this.LastCleanTime = dateTimeDataInstance.Data;
				GenericDictionaryData<StringData, string, MeetingOrganizerEntryData, MeetingOrganizerEntry> genericDictionaryData = new GenericDictionaryData<StringData, string, MeetingOrganizerEntryData, MeetingOrganizerEntry>();
				genericDictionaryData.DeserializeData(reader, componentDataPool);
				this.map = genericDictionaryData.Data;
				this.IsDirty = false;
				if (this.LastCleanTime - (ExDateTime)TimeProvider.UtcNow > MeetingOrganizerInfo.AllowedClockSkew)
				{
					throw new CorruptSyncStateException(new LocalizedString("DeviceBehavior.DeserializeData"), null);
				}
				this.CleanIfNecessary();
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00044518 File Offset: 0x00042718
		private MeetingOrganizerEntry ReadFromStore(GlobalObjectId globalObjectId)
		{
			MeetingOrganizerEntry result = null;
			if (Command.CurrentCommand != null)
			{
				string subject;
				string organizer;
				bool? isOrganizer = MeetingOrganizerValidator.IsOrganizer(Command.CurrentCommand.MailboxSession, globalObjectId, out subject, out organizer);
				result = new MeetingOrganizerEntry(globalObjectId, organizer, isOrganizer, subject);
			}
			return result;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0004454E File Offset: 0x0004274E
		private MeetingOrganizerEntry BuildFromCalendarItem(GlobalObjectId globalObjectId, CalendarItemBase calendarItem)
		{
			return new MeetingOrganizerEntry(globalObjectId, (calendarItem.Organizer == null) ? "<NULL>" : calendarItem.Organizer.EmailAddress, new bool?(calendarItem.IsOrganizer()), calendarItem.Subject);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00044588 File Offset: 0x00042788
		internal void CleanIfNecessary()
		{
			if (this.IgnoreCleanupTimeIntervalCheckForTest || (this.map.Count > 0 && (ExDateTime)TimeProvider.UtcNow - this.LastCleanTime >= MeetingOrganizerInfo.CleanupCheckInterval))
			{
				lock (this.instanceLock)
				{
					List<string> list = null;
					if (this.IgnoreCleanupTimeIntervalCheckForTest || (this.map.Count > 0 && (ExDateTime)TimeProvider.UtcNow - this.LastCleanTime >= MeetingOrganizerInfo.CleanupCheckInterval))
					{
						foreach (KeyValuePair<string, MeetingOrganizerEntry> keyValuePair in this.map)
						{
							if (TimeProvider.UtcNow - keyValuePair.Value.EntryTime >= GlobalSettings.MeetingOrganizerEntryLiveTime)
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(keyValuePair.Key);
							}
						}
						if (list != null)
						{
							foreach (string key in list)
							{
								this.map.Remove(key);
							}
						}
						this.LastCleanTime = (ExDateTime)TimeProvider.UtcNow;
						this.IsDirty = true;
					}
				}
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00044738 File Offset: 0x00042938
		private GlobalObjectId MakeCleanId(GlobalObjectId globalObjectId)
		{
			if (!globalObjectId.IsCleanGlobalObjectId)
			{
				return new GlobalObjectId(globalObjectId.CleanGlobalObjectIdBytes);
			}
			return globalObjectId;
		}

		// Token: 0x040007DF RID: 2015
		private static readonly TimeSpan AllowedClockSkew = TimeSpan.FromDays(1.0);

		// Token: 0x040007E0 RID: 2016
		private object instanceLock = new object();

		// Token: 0x040007E1 RID: 2017
		private Dictionary<string, MeetingOrganizerEntry> map = new Dictionary<string, MeetingOrganizerEntry>();

		// Token: 0x040007E2 RID: 2018
		internal static TimeSpan CleanupCheckInterval = GlobalSettings.MeetingOrganizerCleanupTime;
	}
}
