﻿using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000258 RID: 600
	internal class XmlSerializationWriterSetupComponentInfo : XmlSerializationWriter
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x0005DD8F File Offset: 0x0005BF8F
		public void Write13_SetupComponentInfo(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("SetupComponentInfo", "");
				return;
			}
			base.TopLevelElement();
			this.Write12_SetupComponentInfo("SetupComponentInfo", "", (SetupComponentInfo)o, true, false);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0005DDCC File Offset: 0x0005BFCC
		private void Write12_SetupComponentInfo(string n, string ns, SetupComponentInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SetupComponentInfo)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SetupComponentInfo", "");
			}
			base.WriteAttribute("Name", "", o.Name);
			base.WriteAttribute("AlwaysExecute", "", XmlConvert.ToString(o.AlwaysExecute));
			base.WriteAttribute("IsDatacenterOnly", "", XmlConvert.ToString(o.IsDatacenterOnly));
			base.WriteAttribute("IsDatacenterDedicatedOnly", "", XmlConvert.ToString(o.IsDatacenterDedicatedOnly));
			base.WriteAttribute("IsPartnerHostedOnly", "", XmlConvert.ToString(o.IsPartnerHostedOnly));
			if (o.DatacenterMode != DatacenterMode.Common)
			{
				base.WriteAttribute("DatacenterMode", "", this.Write14_DatacenterMode(o.DatacenterMode));
			}
			base.WriteAttribute("DescriptionId", "", o.DescriptionId);
			ServerTaskInfoCollection serverTasks = o.ServerTasks;
			if (serverTasks != null)
			{
				base.WriteStartElement("ServerTasks", "", null, false);
				for (int i = 0; i < ((ICollection)serverTasks).Count; i++)
				{
					this.Write7_ServerTaskInfo("ServerTaskInfo", "", serverTasks[i], true, false);
				}
				base.WriteEndElement();
			}
			OrgTaskInfoCollection orgTasks = o.OrgTasks;
			if (orgTasks != null)
			{
				base.WriteStartElement("OrgTasks", "", null, false);
				for (int j = 0; j < ((ICollection)orgTasks).Count; j++)
				{
					this.Write10_OrgTaskInfo("OrgTaskInfo", "", orgTasks[j], true, false);
				}
				base.WriteEndElement();
			}
			ServicePlanTaskInfoCollection servicePlanOrgTasks = o.ServicePlanOrgTasks;
			if (servicePlanOrgTasks != null)
			{
				base.WriteStartElement("ServicePlanOrgTasks", "", null, false);
				for (int k = 0; k < ((ICollection)servicePlanOrgTasks).Count; k++)
				{
					this.Write11_ServicePlanTaskInfo("ServicePlanTaskInfo", "", servicePlanOrgTasks[k], true, false);
				}
				base.WriteEndElement();
			}
			TaskInfoCollection tasks = o.Tasks;
			if (tasks != null)
			{
				base.WriteStartElement("Tasks", "", null, false);
				for (int l = 0; l < ((ICollection)tasks).Count; l++)
				{
					this.Write2_TaskInfo("TaskInfo", "", tasks[l], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0005E038 File Offset: 0x0005C238
		private void Write2_TaskInfo(string n, string ns, TaskInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (needType)
			{
				return;
			}
			Type type = o.GetType();
			if (type == typeof(TaskInfo))
			{
				return;
			}
			if (type == typeof(OrgTaskInfo))
			{
				this.Write10_OrgTaskInfo(n, ns, (OrgTaskInfo)o, isNullable, true);
				return;
			}
			if (type == typeof(ServicePlanTaskInfo))
			{
				this.Write11_ServicePlanTaskInfo(n, ns, (ServicePlanTaskInfo)o, isNullable, true);
				return;
			}
			if (type == typeof(ServerTaskInfo))
			{
				this.Write7_ServerTaskInfo(n, ns, (ServerTaskInfo)o, isNullable, true);
				return;
			}
			throw base.CreateUnknownTypeException(o);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0005E0EC File Offset: 0x0005C2EC
		private void Write7_ServerTaskInfo(string n, string ns, ServerTaskInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ServerTaskInfo)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ServerTaskInfo", "");
			}
			base.WriteAttribute("Id", "", o.Id);
			base.WriteAttribute("Component", "", o.Component);
			base.WriteAttribute("ExcludeInDatacenterDedicated", "", XmlConvert.ToString(o.ExcludeInDatacenterDedicated));
			this.Write6_ServerTaskInfoBlock("Install", "", o.Install, false, false);
			this.Write6_ServerTaskInfoBlock("BuildToBuildUpgrade", "", o.BuildToBuildUpgrade, false, false);
			this.Write6_ServerTaskInfoBlock("DisasterRecovery", "", o.DisasterRecovery, false, false);
			this.Write6_ServerTaskInfoBlock("Uninstall", "", o.Uninstall, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0005E1FC File Offset: 0x0005C3FC
		private void Write6_ServerTaskInfoBlock(string n, string ns, ServerTaskInfoBlock o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ServerTaskInfoBlock)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ServerTaskInfoBlock", "");
			}
			base.WriteAttribute("DescriptionId", "", o.DescriptionId);
			base.WriteAttribute("UseInstallTasks", "", XmlConvert.ToString(o.UseInstallTasks));
			base.WriteAttribute("Weight", "", XmlConvert.ToString(o.Weight));
			base.WriteAttribute("IsFatal", "", XmlConvert.ToString(o.IsFatal));
			this.Write5_ServerTaskInfoEntry("Standalone", "", o.Standalone, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0005E2E4 File Offset: 0x0005C4E4
		private void Write5_ServerTaskInfoEntry(string n, string ns, ServerTaskInfoEntry o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ServerTaskInfoEntry)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ServerTaskInfoEntry", "");
			}
			base.WriteAttribute("UseStandaloneTask", "", XmlConvert.ToString(o.UseStandaloneTask));
			if (o.Task != null)
			{
				base.WriteValue(o.Task);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0005E37C File Offset: 0x0005C57C
		private void Write11_ServicePlanTaskInfo(string n, string ns, ServicePlanTaskInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ServicePlanTaskInfo)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ServicePlanTaskInfo", "");
			}
			base.WriteAttribute("FeatureName", "", o.FeatureName);
			this.Write9_OrgTaskInfoBlock("Install", "", o.Install, false, false);
			this.Write9_OrgTaskInfoBlock("BuildToBuildUpgrade", "", o.BuildToBuildUpgrade, false, false);
			this.Write9_OrgTaskInfoBlock("Uninstall", "", o.Uninstall, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0005E444 File Offset: 0x0005C644
		private void Write9_OrgTaskInfoBlock(string n, string ns, OrgTaskInfoBlock o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OrgTaskInfoBlock)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("OrgTaskInfoBlock", "");
			}
			base.WriteAttribute("DescriptionId", "", o.DescriptionId);
			base.WriteAttribute("UseInstallTasks", "", XmlConvert.ToString(o.UseInstallTasks));
			base.WriteAttribute("Weight", "", XmlConvert.ToString(o.Weight));
			base.WriteAttribute("IsFatal", "", XmlConvert.ToString(o.IsFatal));
			this.Write8_OrgTaskInfoEntry("Global", "", o.Global, false, false);
			this.Write8_OrgTaskInfoEntry("Tenant", "", o.Tenant, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0005E544 File Offset: 0x0005C744
		private void Write8_OrgTaskInfoEntry(string n, string ns, OrgTaskInfoEntry o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OrgTaskInfoEntry)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("OrgTaskInfoEntry", "");
			}
			base.WriteAttribute("UseGlobalTask", "", XmlConvert.ToString(o.UseGlobalTask));
			base.WriteAttribute("UseForReconciliation", "", XmlConvert.ToString(o.UseForReconciliation));
			base.WriteAttribute("RecipientOperation", "", XmlConvert.ToString(o.RecipientOperation));
			if (o.Task != null)
			{
				base.WriteValue(o.Task);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0005E614 File Offset: 0x0005C814
		private void Write10_OrgTaskInfo(string n, string ns, OrgTaskInfo o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OrgTaskInfo)))
				{
					if (type == typeof(ServicePlanTaskInfo))
					{
						this.Write11_ServicePlanTaskInfo(n, ns, (ServicePlanTaskInfo)o, isNullable, true);
						return;
					}
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("OrgTaskInfo", "");
			}
			base.WriteAttribute("Id", "", o.Id);
			base.WriteAttribute("Component", "", o.Component);
			base.WriteAttribute("ExcludeInDatacenterDedicated", "", XmlConvert.ToString(o.ExcludeInDatacenterDedicated));
			this.Write9_OrgTaskInfoBlock("Install", "", o.Install, false, false);
			this.Write9_OrgTaskInfoBlock("BuildToBuildUpgrade", "", o.BuildToBuildUpgrade, false, false);
			this.Write9_OrgTaskInfoBlock("Uninstall", "", o.Uninstall, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0005E730 File Offset: 0x0005C930
		private string Write14_DatacenterMode(DatacenterMode v)
		{
			string result;
			switch (v)
			{
			case DatacenterMode.Common:
				result = "Common";
				break;
			case DatacenterMode.Ffo:
				result = "Ffo";
				break;
			case DatacenterMode.ExO:
				result = "ExO";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Management.Deployment.DatacenterMode");
			}
			return result;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0005E789 File Offset: 0x0005C989
		protected override void InitCallbacks()
		{
		}
	}
}
