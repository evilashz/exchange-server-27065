using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000013 RID: 19
	internal class WorkloadPolicy
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003E60 File Offset: 0x00002060
		public WorkloadPolicy(WorkloadType type) : this(type, VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null))
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E78 File Offset: 0x00002078
		public WorkloadPolicy(WorkloadType type, VariantConfigurationSnapshot config) : this(type, config.WorkloadManagement.GetObject<IWorkloadSettings>(type, new object[0]))
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003EA6 File Offset: 0x000020A6
		public WorkloadPolicy(WorkloadType type, IWorkloadSettings settings) : this(type, settings.Classification, settings.MaxConcurrency)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003EBB File Offset: 0x000020BB
		public WorkloadPolicy(WorkloadType type, WorkloadClassification classification, int maxConcurrency)
		{
			this.Type = type;
			this.classification = classification;
			this.maxConcurrency = maxConcurrency;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003ED8 File Offset: 0x000020D8
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003EE0 File Offset: 0x000020E0
		public WorkloadType Type { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003EEC File Offset: 0x000020EC
		public WorkloadClassification Classification
		{
			get
			{
				string text = null;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3368430909U, ref text);
				WorkloadType workloadType;
				WorkloadClassification result;
				int num;
				if (!string.IsNullOrEmpty(text) && this.ParseFaultInjectionState(text, out workloadType, out result, out num))
				{
					return result;
				}
				return this.classification;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003F2C File Offset: 0x0000212C
		public int MaxConcurrency
		{
			get
			{
				string text = null;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3368430909U, ref text);
				WorkloadType workloadType;
				WorkloadClassification workloadClassification;
				int result;
				if (!string.IsNullOrEmpty(text) && this.ParseFaultInjectionState(text, out workloadType, out workloadClassification, out result))
				{
					return result;
				}
				return this.maxConcurrency;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003F6B File Offset: 0x0000216B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WorkloadPolicy);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F79 File Offset: 0x00002179
		public bool Equals(WorkloadPolicy policy)
		{
			return policy != null && this.Type == policy.Type && this.Classification == policy.Classification && this.MaxConcurrency == policy.MaxConcurrency;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FAA File Offset: 0x000021AA
		public override int GetHashCode()
		{
			return (int)(this.Type ^ (WorkloadType)this.classification ^ (WorkloadType)this.maxConcurrency);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FC0 File Offset: 0x000021C0
		public override string ToString()
		{
			string text = this.Type + ':' + this.Classification;
			if (this.MaxConcurrency < 2147483647)
			{
				text = text + ":" + this.MaxConcurrency;
			}
			return text;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004018 File Offset: 0x00002218
		public XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("WorkloadPolicy");
			xelement.Add(new XElement("WorkloadType", this.Type));
			xelement.Add(new XElement("Classification", this.Classification));
			if (this.MaxConcurrency < 2147483647)
			{
				xelement.Add(new XElement("MaxConcurrency", this.MaxConcurrency));
			}
			return xelement;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000040A4 File Offset: 0x000022A4
		private bool ParseFaultInjectionState(string state, out WorkloadType parsedWorkloadType, out WorkloadClassification parsedClassification, out int parsedMaxConcurrency)
		{
			parsedWorkloadType = WorkloadType.Unknown;
			parsedClassification = WorkloadClassification.Unknown;
			parsedMaxConcurrency = -1;
			string[] array = state.Split(new char[]
			{
				'~'
			});
			if (array == null || array.Length != 3)
			{
				ExTraceGlobals.PoliciesTracer.TraceError<string>((long)this.GetHashCode(), "[WorkloadPolicy.ParseFaultInjectionState] Invalid fault injection state: '{0}'.  Using normal values instead.", state);
				return false;
			}
			WorkloadType workloadType;
			if (!Enum.TryParse<WorkloadType>(array[0], out workloadType))
			{
				ExTraceGlobals.PoliciesTracer.TraceError<string, WorkloadType>((long)this.GetHashCode(), "[WorkloadPolicy.ParseFaultInjectionState] Failed to parse WorkloadType.  Value: {0}.  Using {1} instead.", array[0], this.Type);
				return false;
			}
			if (workloadType != this.Type)
			{
				ExTraceGlobals.PoliciesTracer.TraceDebug<WorkloadType, WorkloadType>((long)this.GetHashCode(), "[WorkloadPolicy.ParseFaultInjectionState] Fault injection state refers to workload type {0} while this instance is for type {1}.  Ignoring", workloadType, this.Type);
				return false;
			}
			WorkloadClassification workloadClassification;
			if (!Enum.TryParse<WorkloadClassification>(array[1], out workloadClassification))
			{
				ExTraceGlobals.PoliciesTracer.TraceError<string, WorkloadClassification>((long)this.GetHashCode(), "[WorkloadPolicy.ParseFaultInjectionState] Failed to parse WorkloadClassification.  Value: {0}.  Using {1} instead.", array[1], this.classification);
				return false;
			}
			if (!int.TryParse(array[2], out parsedMaxConcurrency))
			{
				ExTraceGlobals.PoliciesTracer.TraceError<string, int>((long)this.GetHashCode(), "[WorkloadPolicy.ParseFaultInjectionState] Failed to parse MaxConcurrency. Value: {0}.  Using {1} instead.", array[2], this.maxConcurrency);
				return false;
			}
			parsedWorkloadType = workloadType;
			parsedClassification = workloadClassification;
			if (parsedMaxConcurrency < 0)
			{
				parsedMaxConcurrency = this.maxConcurrency;
			}
			return true;
		}

		// Token: 0x04000052 RID: 82
		private const string ProcessAccessManagerComponentName = "WorkloadPolicy";

		// Token: 0x04000053 RID: 83
		private const uint LidWorkloadClassification = 3368430909U;

		// Token: 0x04000054 RID: 84
		private readonly WorkloadClassification classification;

		// Token: 0x04000055 RID: 85
		private readonly int maxConcurrency;
	}
}
