using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200002A RID: 42
	[Table]
	public sealed class ResponderResult : WorkItemResult, IPersistence, IWorkItemResultSerialization
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000C126 File Offset: 0x0000A326
		public ResponderResult(WorkDefinition definition) : base(definition)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C12F File Offset: 0x0000A32F
		public ResponderResult()
		{
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000C137 File Offset: 0x0000A337
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000C13F File Offset: 0x0000A33F
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int ResultId { get; protected internal set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000C148 File Offset: 0x0000A348
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000C150 File Offset: 0x0000A350
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C159 File Offset: 0x0000A359
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000C161 File Offset: 0x0000A361
		[Column]
		public override bool IsNotified { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C16A File Offset: 0x0000A36A
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000C172 File Offset: 0x0000A372
		[Column]
		public override string ResultName { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C17B File Offset: 0x0000A37B
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000C183 File Offset: 0x0000A383
		[Column]
		public override int WorkItemId { get; internal set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C18C File Offset: 0x0000A38C
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000C194 File Offset: 0x0000A394
		[Column]
		public override int DeploymentId { get; internal set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000C19D File Offset: 0x0000A39D
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000C1A5 File Offset: 0x0000A3A5
		[Column]
		public override string MachineName { get; internal set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C1AE File Offset: 0x0000A3AE
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000C1B6 File Offset: 0x0000A3B6
		[Column]
		public override string Error { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C1BF File Offset: 0x0000A3BF
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000C1C7 File Offset: 0x0000A3C7
		[Column]
		public override string Exception { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		[Column]
		public override byte RetryCount { get; internal set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000C1E9 File Offset: 0x0000A3E9
		[Column]
		public override string StateAttribute1 { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000C1FA File Offset: 0x0000A3FA
		[Column]
		public override string StateAttribute2 { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000C203 File Offset: 0x0000A403
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000C20B File Offset: 0x0000A40B
		[Column]
		public override string StateAttribute3 { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000C214 File Offset: 0x0000A414
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000C21C File Offset: 0x0000A41C
		[Column]
		public override string StateAttribute4 { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000C225 File Offset: 0x0000A425
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000C22D File Offset: 0x0000A42D
		[Column]
		public override string StateAttribute5 { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000C236 File Offset: 0x0000A436
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000C23E File Offset: 0x0000A43E
		[Column]
		public override double StateAttribute6 { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000C247 File Offset: 0x0000A447
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000C24F File Offset: 0x0000A44F
		[Column]
		public override double StateAttribute7 { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000C258 File Offset: 0x0000A458
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000C260 File Offset: 0x0000A460
		[Column]
		public override double StateAttribute8 { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000C269 File Offset: 0x0000A469
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000C271 File Offset: 0x0000A471
		[Column]
		public override double StateAttribute9 { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000C27A File Offset: 0x0000A47A
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000C282 File Offset: 0x0000A482
		[Column]
		public override double StateAttribute10 { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C28B File Offset: 0x0000A48B
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000C293 File Offset: 0x0000A493
		[Column]
		public override ResultType ResultType { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000C29C File Offset: 0x0000A49C
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		[Column]
		public override int ExecutionId { get; protected set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		[Column]
		public override DateTime ExecutionStartTime { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C2BE File Offset: 0x0000A4BE
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
		[Column]
		public override DateTime ExecutionEndTime { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C2CF File Offset: 0x0000A4CF
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		[Column]
		public override byte PoisonedCount { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		[Column]
		public bool IsThrottled { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C2F1 File Offset: 0x0000A4F1
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000C2F9 File Offset: 0x0000A4F9
		[Column]
		public string ResponseResource { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C302 File Offset: 0x0000A502
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C30A File Offset: 0x0000A50A
		[Column]
		public string ResponseAction { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C313 File Offset: 0x0000A513
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C31B File Offset: 0x0000A51B
		[Column]
		public ServiceHealthStatus TargetHealthState { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000C32C File Offset: 0x0000A52C
		[Column]
		public int TargetHealthStateTransitionId { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000C335 File Offset: 0x0000A535
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000C33D File Offset: 0x0000A53D
		[Column]
		public DateTime? FirstAlertObservedTime { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000C346 File Offset: 0x0000A546
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000C34E File Offset: 0x0000A54E
		[Column]
		public ServiceRecoveryResult RecoveryResult { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000C357 File Offset: 0x0000A557
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000C35F File Offset: 0x0000A55F
		[Column]
		public bool IsRecoveryAttempted { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000C368 File Offset: 0x0000A568
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000C370 File Offset: 0x0000A570
		[Column]
		public string CorrelationResultsXml { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000C379 File Offset: 0x0000A579
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000C381 File Offset: 0x0000A581
		[Column]
		public CorrelatedMonitorAction CorrelationAction { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000C38A File Offset: 0x0000A58A
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000C392 File Offset: 0x0000A592
		[Column]
		internal override int Version { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		internal static int SchemaVersion
		{
			get
			{
				return ResponderResult.schemaVersion;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C3B3 File Offset: 0x0000A5B3
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000C3BA File Offset: 0x0000A5BA
		internal static Action<ResponderResult> ResultWriter { private get; set; }

		// Token: 0x06000316 RID: 790 RVA: 0x0000C3C2 File Offset: 0x0000A5C2
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("ResultId", out text) && !string.IsNullOrEmpty(text))
			{
				this.ResultId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ServiceName", out text))
			{
				this.ServiceName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("IsNotified", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsNotified = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("ResultName", out text))
			{
				this.ResultName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WorkItemId", out text) && !string.IsNullOrEmpty(text))
			{
				this.WorkItemId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("DeploymentId", out text) && !string.IsNullOrEmpty(text))
			{
				this.DeploymentId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("MachineName", out text))
			{
				this.MachineName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Error", out text))
			{
				this.Error = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Exception", out text))
			{
				this.Exception = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("RetryCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.RetryCount = byte.Parse(text);
			}
			if (propertyBag.TryGetValue("StateAttribute1", out text))
			{
				this.StateAttribute1 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute2", out text))
			{
				this.StateAttribute2 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute3", out text))
			{
				this.StateAttribute3 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute4", out text))
			{
				this.StateAttribute4 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute5", out text))
			{
				this.StateAttribute5 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute6", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute6 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute7", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute7 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute8", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute8 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute9", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute9 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute10", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute10 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("ResultType", out text) && !string.IsNullOrEmpty(text))
			{
				this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), text);
			}
			if (propertyBag.TryGetValue("ExecutionId", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionStartTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionStartTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("ExecutionEndTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionEndTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("PoisonedCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.PoisonedCount = byte.Parse(text);
			}
			if (propertyBag.TryGetValue("IsThrottled", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsThrottled = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("ResponseResource", out text))
			{
				this.ResponseResource = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ResponseAction", out text))
			{
				this.ResponseAction = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetHealthState", out text) && !string.IsNullOrEmpty(text))
			{
				this.TargetHealthState = (ServiceHealthStatus)Enum.Parse(typeof(ServiceHealthStatus), text);
			}
			if (propertyBag.TryGetValue("TargetHealthStateTransitionId", out text) && !string.IsNullOrEmpty(text))
			{
				this.TargetHealthStateTransitionId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("FirstAlertObservedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.FirstAlertObservedTime = (string.Equals(text, "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(text).ToUniversalTime()));
			}
			if (propertyBag.TryGetValue("RecoveryResult", out text) && !string.IsNullOrEmpty(text))
			{
				this.RecoveryResult = (ServiceRecoveryResult)Enum.Parse(typeof(ServiceRecoveryResult), text);
			}
			if (propertyBag.TryGetValue("IsRecoveryAttempted", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsRecoveryAttempted = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("CorrelationResultsXml", out text))
			{
				this.CorrelationResultsXml = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CorrelationAction", out text) && !string.IsNullOrEmpty(text))
			{
				this.CorrelationAction = (CorrelatedMonitorAction)Enum.Parse(typeof(CorrelatedMonitorAction), text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public override void AssignResultId()
		{
			if (this.ResultId == 0)
			{
				this.ResultId = ResponderResult.idGenerator.NextId();
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			this.AssignResultId();
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			if (ResponderResult.ResultWriter != null)
			{
				ResponderResult.ResultWriter(this);
				return;
			}
			NativeMethods.ResponderResultUnmanaged responderResultUnmanaged = this.ToUnmanaged();
			ResultSeverityLevel severity = CrimsonHelper.ConvertResultTypeToSeverityLevel(this.ResultType);
			NativeMethods.WriteResponderResult(ref responderResultUnmanaged, severity);
			LocalDataAccess.ResponderResultLogger.LogEvent(DateTime.UtcNow, this.ToDictionary());
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C955 File Offset: 0x0000AB55
		public string Serialize()
		{
			return CrimsonHelper.Serialize(this.ToDictionary(), false);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C964 File Offset: 0x0000AB64
		public void Deserialize(string result)
		{
			string[] array = CrimsonHelper.ClearResultString(result).Split(new char[]
			{
				'|'
			});
			if (!string.IsNullOrEmpty(array[0]))
			{
				this.ResultId = int.Parse(array[0]);
			}
			this.ServiceName = CrimsonHelper.NullDecode(array[1]);
			if (!string.IsNullOrEmpty(array[2]))
			{
				this.IsNotified = bool.Parse(array[2]);
			}
			this.ResultName = CrimsonHelper.NullDecode(array[3]);
			if (!string.IsNullOrEmpty(array[4]))
			{
				this.WorkItemId = int.Parse(array[4]);
			}
			if (!string.IsNullOrEmpty(array[5]))
			{
				this.DeploymentId = int.Parse(array[5]);
			}
			this.MachineName = CrimsonHelper.NullDecode(array[6]);
			this.Error = CrimsonHelper.NullDecode(array[7]);
			this.Exception = CrimsonHelper.NullDecode(array[8]);
			if (!string.IsNullOrEmpty(array[9]))
			{
				this.RetryCount = byte.Parse(array[9]);
			}
			this.StateAttribute1 = CrimsonHelper.NullDecode(array[10]);
			this.StateAttribute2 = CrimsonHelper.NullDecode(array[11]);
			this.StateAttribute3 = CrimsonHelper.NullDecode(array[12]);
			this.StateAttribute4 = CrimsonHelper.NullDecode(array[13]);
			this.StateAttribute5 = CrimsonHelper.NullDecode(array[14]);
			if (!string.IsNullOrEmpty(array[15]))
			{
				this.StateAttribute6 = CrimsonHelper.ParseDouble(array[15]);
			}
			if (!string.IsNullOrEmpty(array[16]))
			{
				this.StateAttribute7 = CrimsonHelper.ParseDouble(array[16]);
			}
			if (!string.IsNullOrEmpty(array[17]))
			{
				this.StateAttribute8 = CrimsonHelper.ParseDouble(array[17]);
			}
			if (!string.IsNullOrEmpty(array[18]))
			{
				this.StateAttribute9 = CrimsonHelper.ParseDouble(array[18]);
			}
			if (!string.IsNullOrEmpty(array[19]))
			{
				this.StateAttribute10 = CrimsonHelper.ParseDouble(array[19]);
			}
			if (!string.IsNullOrEmpty(array[20]))
			{
				this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), array[20]);
			}
			if (!string.IsNullOrEmpty(array[21]))
			{
				this.ExecutionId = int.Parse(array[21]);
			}
			if (!string.IsNullOrEmpty(array[22]))
			{
				this.ExecutionStartTime = DateTime.Parse(array[22]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[23]))
			{
				this.ExecutionEndTime = DateTime.Parse(array[23]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[24]))
			{
				this.PoisonedCount = byte.Parse(array[24]);
			}
			if (!string.IsNullOrEmpty(array[25]))
			{
				this.IsThrottled = bool.Parse(array[25]);
			}
			this.ResponseResource = CrimsonHelper.NullDecode(array[26]);
			this.ResponseAction = CrimsonHelper.NullDecode(array[27]);
			if (!string.IsNullOrEmpty(array[28]))
			{
				this.TargetHealthState = (ServiceHealthStatus)Enum.Parse(typeof(ServiceHealthStatus), array[28]);
			}
			if (!string.IsNullOrEmpty(array[29]))
			{
				this.TargetHealthStateTransitionId = int.Parse(array[29]);
			}
			if (!string.IsNullOrEmpty(array[30]))
			{
				this.FirstAlertObservedTime = (string.Equals(array[30], "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(array[30]).ToUniversalTime()));
			}
			if (!string.IsNullOrEmpty(array[31]))
			{
				this.RecoveryResult = (ServiceRecoveryResult)Enum.Parse(typeof(ServiceRecoveryResult), array[31]);
			}
			if (!string.IsNullOrEmpty(array[32]))
			{
				this.IsRecoveryAttempted = bool.Parse(array[32]);
			}
			this.CorrelationResultsXml = CrimsonHelper.NullDecode(array[33]);
			if (!string.IsNullOrEmpty(array[34]))
			{
				this.CorrelationAction = (CorrelatedMonitorAction)Enum.Parse(typeof(CorrelatedMonitorAction), array[34]);
			}
			if (!string.IsNullOrEmpty(array[35]))
			{
				this.Version = int.Parse(array[35]);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000CD10 File Offset: 0x0000AF10
		internal NativeMethods.ResponderResultUnmanaged ToUnmanaged()
		{
			return new NativeMethods.ResponderResultUnmanaged
			{
				ResultId = this.ResultId,
				ServiceName = CrimsonHelper.NullCode(this.ServiceName),
				IsNotified = (this.IsNotified ? 1 : 0),
				ResultName = CrimsonHelper.NullCode(this.ResultName),
				WorkItemId = this.WorkItemId,
				DeploymentId = this.DeploymentId,
				MachineName = CrimsonHelper.NullCode(this.MachineName),
				Error = CrimsonHelper.NullCode(this.Error),
				Exception = CrimsonHelper.NullCode(this.Exception),
				RetryCount = this.RetryCount,
				StateAttribute1 = CrimsonHelper.NullCode(this.StateAttribute1),
				StateAttribute2 = CrimsonHelper.NullCode(this.StateAttribute2),
				StateAttribute3 = CrimsonHelper.NullCode(this.StateAttribute3),
				StateAttribute4 = CrimsonHelper.NullCode(this.StateAttribute4),
				StateAttribute5 = CrimsonHelper.NullCode(this.StateAttribute5),
				StateAttribute6 = this.StateAttribute6,
				StateAttribute7 = this.StateAttribute7,
				StateAttribute8 = this.StateAttribute8,
				StateAttribute9 = this.StateAttribute9,
				StateAttribute10 = this.StateAttribute10,
				ResultType = this.ResultType,
				ExecutionId = this.ExecutionId,
				ExecutionStartTime = this.ExecutionStartTime.ToUniversalTime().ToString("o"),
				ExecutionEndTime = this.ExecutionEndTime.ToUniversalTime().ToString("o"),
				PoisonedCount = this.PoisonedCount,
				IsThrottled = (this.IsThrottled ? 1 : 0),
				ResponseResource = CrimsonHelper.NullCode(this.ResponseResource),
				ResponseAction = CrimsonHelper.NullCode(this.ResponseAction),
				TargetHealthState = this.TargetHealthState,
				TargetHealthStateTransitionId = this.TargetHealthStateTransitionId,
				FirstAlertObservedTime = ((this.FirstAlertObservedTime != null) ? this.FirstAlertObservedTime.Value.ToUniversalTime().ToString("o") : "[null]"),
				RecoveryResult = this.RecoveryResult,
				IsRecoveryAttempted = (this.IsRecoveryAttempted ? 1 : 0),
				CorrelationResultsXml = CrimsonHelper.NullCode(this.CorrelationResultsXml),
				CorrelationAction = this.CorrelationAction,
				Version = this.Version
			};
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		internal Dictionary<string, object> ToDictionary()
		{
			return new Dictionary<string, object>(50)
			{
				{
					"ResultId",
					this.ResultId
				},
				{
					"ServiceName",
					this.ServiceName
				},
				{
					"IsNotified",
					this.IsNotified
				},
				{
					"ResultName",
					this.ResultName
				},
				{
					"WorkItemId",
					this.WorkItemId
				},
				{
					"DeploymentId",
					this.DeploymentId
				},
				{
					"MachineName",
					this.MachineName
				},
				{
					"Error",
					this.Error
				},
				{
					"Exception",
					this.Exception
				},
				{
					"RetryCount",
					this.RetryCount
				},
				{
					"StateAttribute1",
					this.StateAttribute1
				},
				{
					"StateAttribute2",
					this.StateAttribute2
				},
				{
					"StateAttribute3",
					this.StateAttribute3
				},
				{
					"StateAttribute4",
					this.StateAttribute4
				},
				{
					"StateAttribute5",
					this.StateAttribute5
				},
				{
					"StateAttribute6",
					this.StateAttribute6
				},
				{
					"StateAttribute7",
					this.StateAttribute7
				},
				{
					"StateAttribute8",
					this.StateAttribute8
				},
				{
					"StateAttribute9",
					this.StateAttribute9
				},
				{
					"StateAttribute10",
					this.StateAttribute10
				},
				{
					"ResultType",
					this.ResultType
				},
				{
					"ExecutionId",
					this.ExecutionId
				},
				{
					"ExecutionStartTime",
					this.ExecutionStartTime
				},
				{
					"ExecutionEndTime",
					this.ExecutionEndTime
				},
				{
					"PoisonedCount",
					this.PoisonedCount
				},
				{
					"IsThrottled",
					this.IsThrottled
				},
				{
					"ResponseResource",
					this.ResponseResource
				},
				{
					"ResponseAction",
					this.ResponseAction
				},
				{
					"TargetHealthState",
					this.TargetHealthState
				},
				{
					"TargetHealthStateTransitionId",
					this.TargetHealthStateTransitionId
				},
				{
					"FirstAlertObservedTime",
					this.FirstAlertObservedTime
				},
				{
					"RecoveryResult",
					this.RecoveryResult
				},
				{
					"IsRecoveryAttempted",
					this.IsRecoveryAttempted
				},
				{
					"CorrelationResultsXml",
					this.CorrelationResultsXml
				},
				{
					"CorrelationAction",
					this.CorrelationAction
				},
				{
					"Version",
					this.Version
				}
			};
		}

		// Token: 0x04000276 RID: 630
		private static int schemaVersion = 65536;

		// Token: 0x04000277 RID: 631
		private static ResponderResultIdGenerator idGenerator = new ResponderResultIdGenerator();
	}
}
