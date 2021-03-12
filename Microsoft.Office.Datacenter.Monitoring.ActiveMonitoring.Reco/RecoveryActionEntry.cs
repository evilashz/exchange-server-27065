using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class RecoveryActionEntry : IPersistence
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00007561 File Offset: 0x00005761
		public RecoveryActionEntry()
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000756C File Offset: 0x0000576C
		internal RecoveryActionEntry(RecoveryActionHelper.RecoveryActionEntrySerializable entry)
		{
			this.Id = entry.Id;
			this.InstanceId = entry.InstanceId;
			this.ResourceName = entry.ResourceName;
			this.StartTime = entry.StartTime;
			this.EndTime = entry.EndTime;
			this.State = entry.State;
			this.Result = entry.Result;
			this.RequestorName = entry.RequestorName;
			this.ExceptionName = entry.ExceptionName;
			this.ExceptionMessage = entry.ExceptionMessage;
			this.Context = entry.Context;
			this.CustomArg1 = entry.CustomArg1;
			this.CustomArg2 = entry.CustomArg2;
			this.CustomArg3 = entry.CustomArg3;
			this.LamProcessStartTime = entry.LamProcessStartTime;
			this.ThrottleIdentity = entry.ThrottleIdentity;
			this.ThrottleParametersXml = entry.ThrottleParametersXml;
			this.TotalLocalActionsInOneHour = entry.TotalLocalActionsInOneHour;
			this.TotalLocalActionsInOneDay = entry.TotalLocalActionsInOneDay;
			this.TotalGroupActionsInOneDay = entry.TotalGroupActionsInOneDay;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000766F File Offset: 0x0000586F
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00007677 File Offset: 0x00005877
		public bool IsReadFromPersistentStore { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007680 File Offset: 0x00005880
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00007688 File Offset: 0x00005888
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007691 File Offset: 0x00005891
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00007699 File Offset: 0x00005899
		public RecoveryActionId Id { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000076A2 File Offset: 0x000058A2
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000076AA File Offset: 0x000058AA
		public string InstanceId { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000076B3 File Offset: 0x000058B3
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000076BB File Offset: 0x000058BB
		public string ResourceName { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000076C4 File Offset: 0x000058C4
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000076CC File Offset: 0x000058CC
		public DateTime StartTime { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000076D5 File Offset: 0x000058D5
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000076DD File Offset: 0x000058DD
		public DateTime EndTime { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000076E6 File Offset: 0x000058E6
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000076EE File Offset: 0x000058EE
		public RecoveryActionState State { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000076F7 File Offset: 0x000058F7
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000076FF File Offset: 0x000058FF
		public RecoveryActionResult Result { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007708 File Offset: 0x00005908
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00007710 File Offset: 0x00005910
		public string RequestorName { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007719 File Offset: 0x00005919
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00007721 File Offset: 0x00005921
		public string ExceptionName { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000772A File Offset: 0x0000592A
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00007732 File Offset: 0x00005932
		public string ExceptionMessage { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000773B File Offset: 0x0000593B
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00007743 File Offset: 0x00005943
		public string Context { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000774C File Offset: 0x0000594C
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007754 File Offset: 0x00005954
		public string CustomArg1 { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000775D File Offset: 0x0000595D
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007765 File Offset: 0x00005965
		public string CustomArg2 { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000776E File Offset: 0x0000596E
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007776 File Offset: 0x00005976
		public string CustomArg3 { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000777F File Offset: 0x0000597F
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007787 File Offset: 0x00005987
		public DateTime LamProcessStartTime { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007790 File Offset: 0x00005990
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00007798 File Offset: 0x00005998
		public int WriteRecordDelayInMilliSeconds { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000077A1 File Offset: 0x000059A1
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000077A9 File Offset: 0x000059A9
		public string ThrottleIdentity { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000077B2 File Offset: 0x000059B2
		// (set) Token: 0x0600024C RID: 588 RVA: 0x000077BA File Offset: 0x000059BA
		public string ThrottleParametersXml { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000077C3 File Offset: 0x000059C3
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000077CB File Offset: 0x000059CB
		public int TotalLocalActionsInOneHour { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000077D4 File Offset: 0x000059D4
		// (set) Token: 0x06000250 RID: 592 RVA: 0x000077DC File Offset: 0x000059DC
		public int TotalLocalActionsInOneDay { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000077E5 File Offset: 0x000059E5
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000077ED File Offset: 0x000059ED
		public int TotalGroupActionsInOneDay { get; set; }

		// Token: 0x06000253 RID: 595 RVA: 0x000077F6 File Offset: 0x000059F6
		public string GetName()
		{
			return RecoveryActionHelper.ConstructActionName(this.Id, this.ResourceName);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007809 File Offset: 0x00005A09
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.IsReadFromPersistentStore = true;
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007820 File Offset: 0x00005A20
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			this.Id = this.GetEnumProperty<RecoveryActionId>(propertyBag, "Id");
			this.InstanceId = this.GetStringProperty(propertyBag, "InstanceId");
			this.ResourceName = this.GetStringProperty(propertyBag, "ResourceName");
			this.StartTime = this.GetDateTimeProperty(propertyBag, "StartTime");
			this.EndTime = this.GetDateTimeProperty(propertyBag, "EndTime");
			this.State = this.GetEnumProperty<RecoveryActionState>(propertyBag, "State");
			this.Result = this.GetEnumProperty<RecoveryActionResult>(propertyBag, "Result");
			this.RequestorName = this.GetStringProperty(propertyBag, "RequestorName");
			this.ExceptionName = this.GetStringProperty(propertyBag, "ExceptionName");
			this.ExceptionMessage = this.GetStringProperty(propertyBag, "ExceptionMessage");
			this.Context = this.GetStringProperty(propertyBag, "Context");
			this.CustomArg1 = this.GetStringProperty(propertyBag, "CustomArg1");
			this.CustomArg2 = this.GetStringProperty(propertyBag, "CustomArg2");
			this.CustomArg3 = this.GetStringProperty(propertyBag, "CustomArg3");
			this.LamProcessStartTime = this.GetDateTimeProperty(propertyBag, "LamProcessStartTime");
			this.ThrottleIdentity = this.GetStringProperty(propertyBag, "ThrottleIdentity");
			this.ThrottleParametersXml = this.GetStringProperty(propertyBag, "ThrottleParametersXml");
			this.TotalLocalActionsInOneHour = this.GetIntProperty(propertyBag, "TotalLocalActionsInOneHour");
			this.TotalLocalActionsInOneDay = this.GetIntProperty(propertyBag, "TotalLocalActionsInOneDay");
			this.TotalGroupActionsInOneDay = this.GetIntProperty(propertyBag, "TotalGroupActionsInOneDay");
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007998 File Offset: 0x00005B98
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			if (this.IsReadFromPersistentStore)
			{
				throw new InvalidOperationException();
			}
			if (this.WriteRecordDelayInMilliSeconds > 0)
			{
				Thread.Sleep(this.WriteRecordDelayInMilliSeconds);
			}
			ManagedAvailabilityCrimsonEvent crimsonEventToUse = this.GetCrimsonEventToUse();
			crimsonEventToUse.LogGeneric(new object[]
			{
				this.Id,
				this.InstanceId,
				CrimsonHelper.NullCode(this.ResourceName),
				this.ToUniversalSortableTimeString(this.StartTime),
				this.ToUniversalSortableTimeString(this.EndTime),
				this.State,
				this.Result,
				CrimsonHelper.NullCode(this.RequestorName),
				CrimsonHelper.NullCode(this.ExceptionName),
				CrimsonHelper.NullCode(this.ExceptionMessage),
				CrimsonHelper.NullCode(this.Context),
				CrimsonHelper.NullCode(this.CustomArg1),
				CrimsonHelper.NullCode(this.CustomArg2),
				CrimsonHelper.NullCode(this.CustomArg3),
				this.LamProcessStartTime,
				CrimsonHelper.NullCode(this.ThrottleIdentity),
				CrimsonHelper.NullCode(this.ThrottleParametersXml),
				this.TotalLocalActionsInOneHour,
				this.TotalLocalActionsInOneDay,
				this.TotalGroupActionsInOneDay
			});
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007B00 File Offset: 0x00005D00
		public Dictionary<string, object> ToDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["Id"] = this.Id;
			dictionary["InstanceId"] = this.InstanceId;
			dictionary["ResourceName"] = this.ResourceName;
			dictionary["StartTime"] = this.StartTime;
			dictionary["EndTime"] = this.EndTime;
			dictionary["State"] = this.State;
			dictionary["Result"] = this.Result;
			dictionary["RequestorName"] = this.RequestorName;
			dictionary["ExceptionName"] = this.ExceptionName;
			dictionary["ExceptionMessage"] = this.ExceptionMessage;
			dictionary["Context"] = this.Context;
			dictionary["CustomArg1"] = this.CustomArg1;
			dictionary["CustomArg2"] = this.CustomArg2;
			dictionary["CustomArg3"] = this.CustomArg3;
			dictionary["LamProcessStartTime"] = this.LamProcessStartTime;
			dictionary["ThrottleIdentity"] = this.ThrottleIdentity;
			dictionary["ThrottleParametersXml"] = this.ThrottleParametersXml;
			dictionary["TotalLocalActionsInOneHour"] = this.TotalLocalActionsInOneHour;
			dictionary["TotalLocalActionsInOneDay"] = this.TotalLocalActionsInOneDay;
			dictionary["TotalGroupActionsInOneDay"] = this.TotalGroupActionsInOneDay;
			return dictionary;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007C98 File Offset: 0x00005E98
		private ManagedAvailabilityCrimsonEvent GetCrimsonEventToUse()
		{
			ManagedAvailabilityCrimsonEvent result = null;
			if (this.State == RecoveryActionState.Started)
			{
				result = ManagedAvailabilityCrimsonEvents.RecoveryStarted;
			}
			else if (this.State == RecoveryActionState.Finished)
			{
				if (this.Result == RecoveryActionResult.Succeeded)
				{
					result = ManagedAvailabilityCrimsonEvents.RecoverySucceeded;
				}
				else
				{
					result = ManagedAvailabilityCrimsonEvents.RecoveryFailed;
				}
			}
			return result;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007CDC File Offset: 0x00005EDC
		private string ToUniversalSortableTimeString(DateTime dateTime)
		{
			return dateTime.ToUniversalTime().ToString("o");
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007D00 File Offset: 0x00005F00
		private string GetStringProperty(Dictionary<string, string> propertyBag, string propertyName)
		{
			string text = null;
			propertyBag.TryGetValue(propertyName, out text);
			text = CrimsonHelper.NullDecode(text);
			return text;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007D24 File Offset: 0x00005F24
		private DateTime GetDateTimeProperty(Dictionary<string, string> propertyBag, string propertyName)
		{
			DateTime result = DateTime.MinValue;
			string text;
			if (propertyBag.TryGetValue(propertyName, out text) && !string.IsNullOrEmpty(text))
			{
				result = DateTime.Parse(text);
			}
			return result;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007D54 File Offset: 0x00005F54
		private T GetEnumProperty<T>(Dictionary<string, string> propertyBag, string propertyName)
		{
			T result = default(T);
			string value;
			if (propertyBag.TryGetValue(propertyName, out value) && !string.IsNullOrEmpty(value))
			{
				result = (T)((object)Enum.Parse(typeof(T), value));
			}
			return result;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007D94 File Offset: 0x00005F94
		private int GetIntProperty(Dictionary<string, string> propertyBag, string propertyName)
		{
			int result = 0;
			string text;
			if (propertyBag.TryGetValue(propertyName, out text) && !string.IsNullOrEmpty(text))
			{
				result = int.Parse(text);
			}
			return result;
		}

		// Token: 0x04000162 RID: 354
		public const int MaximumAllowedElementsPerQuery = 4096;
	}
}
