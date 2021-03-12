using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol
{
	// Token: 0x02000059 RID: 89
	public class FaultDefinition : ResultBase
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000BDFC File Offset: 0x00009FFC
		static FaultDefinition()
		{
			FaultDefinition.description.ComplianceStructureId = 12;
			FaultDefinition.description.RegisterComplexCollectionAccessor<FaultRecord>(0, (FaultDefinition item) => item.Faults.Count, (FaultDefinition item, int index) => item.Faults.ToList<FaultRecord>()[index], delegate(FaultDefinition item, FaultRecord value, int index)
			{
				item.Faults.TryAdd(value);
			}, FaultRecord.Description);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000BE86 File Offset: 0x0000A086
		public static ComplianceSerializationDescription<FaultDefinition> Description
		{
			get
			{
				return FaultDefinition.description;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000BE8D File Offset: 0x0000A08D
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000BE95 File Offset: 0x0000A095
		public bool IsFatalFailure { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public override int SerializationVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public static FaultDefinition FromErrorString(string error, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			FaultDefinition faultDefinition = new FaultDefinition();
			FaultRecord faultRecord = new FaultRecord();
			faultDefinition.Faults.TryAdd(faultRecord);
			faultRecord.Data["RC"] = "0";
			faultRecord.Data["EFILE"] = callerFilePath;
			faultRecord.Data["EFUNC"] = callerMember;
			faultRecord.Data["ELINE"] = callerLineNumber.ToString();
			faultRecord.Data["TEX"] = "false";
			faultRecord.Data["HEX"] = "true";
			faultRecord.Data["EX"] = error;
			faultRecord.Data["EXC"] = string.Format("{0}:{1}:{2}", callerMember, callerLineNumber, error.GetHashCode());
			return faultDefinition;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000BF80 File Offset: 0x0000A180
		public static FaultDefinition FromException(Exception error, bool handled = true, bool transient = false, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			FaultDefinition faultDefinition = new FaultDefinition();
			FaultRecord faultRecord = new FaultRecord();
			faultDefinition.Faults.TryAdd(faultRecord);
			faultRecord.Data["RC"] = "0";
			faultRecord.Data["EFILE"] = callerFilePath;
			faultRecord.Data["EFUNC"] = callerMember;
			faultRecord.Data["ELINE"] = callerLineNumber.ToString();
			faultRecord.Data["TEX"] = transient.ToString();
			faultRecord.Data["HEX"] = handled.ToString();
			faultRecord.Data["EX"] = string.Format("{0}", error);
			if (error != null)
			{
				faultRecord.Data["EXC"] = string.Format("{0}", error.GetType().Name);
			}
			return faultDefinition;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C067 File Offset: 0x0000A267
		public void Merge(FaultDefinition source)
		{
			if (source != null)
			{
				this.MergeFaults(source);
				if (!this.IsFatalFailure)
				{
					this.IsFatalFailure = source.IsFatalFailure;
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C088 File Offset: 0x0000A288
		public WorkPayload ToPayload()
		{
			return new WorkPayload
			{
				WorkDefinitionType = WorkDefinitionType.Fault,
				WorkDefinition = this.GetSerializedResult()
			};
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public override byte[] GetSerializedResult()
		{
			return ComplianceSerializer.Serialize<FaultDefinition>(FaultDefinition.Description, this);
		}

		// Token: 0x04000205 RID: 517
		private static ComplianceSerializationDescription<FaultDefinition> description = new ComplianceSerializationDescription<FaultDefinition>();
	}
}
