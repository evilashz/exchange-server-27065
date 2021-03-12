using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000222 RID: 546
	public static class FFOMigrationStatusService
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x0007BE80 File Offset: 0x0007A080
		public static void MigrationReportPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			dataRow["ShowError"] = false;
			dataRow["ProblemsMigrating"] = true;
			try
			{
				string text = dataRow["Report"].ToStringWithNull();
				XDocument xdocument = XDocument.Parse(text);
				IEnumerable<XElement> source = from y in xdocument.Descendants("Step")
				select y;
				DateTime dateTime = Convert.ToDateTime(source.First<XElement>().Attribute("StartTime").Value.ToString());
				DateTime dateTime2 = Convert.ToDateTime(source.Last<XElement>().Attribute("EndTime").Value.ToString());
				dataRow["StartedOn"] = dateTime.ToString("g");
				dataRow["CompletedOn"] = dateTime2.ToString("g");
				IEnumerable<XElement> source2 = from x in xdocument.Descendants("Step")
				where Convert.ToInt32(x.Attribute("ToState").Value) >= 100
				select x;
				IEnumerable<FFOMigrationStatusService.MigrationStep> enumerable = from e in source2.Elements("Prop")
				where e.Attribute("PropID").Value == "10003"
				select new FFOMigrationStatusService.MigrationStep(e.Parent.Attribute("StepName").Value, e.Parent.Attribute("ToState").Value, e.Attribute("Value").Value);
				if (enumerable.Count<FFOMigrationStatusService.MigrationStep>() >= 1)
				{
					FFOMigrationStatusService.PopulateStepErrors(enumerable, dataRow, "SpamQuarantineMigrationStep", "SpamQuarantineGroupData", "SpamQuarantineGroupVisible");
					FFOMigrationStatusService.PopulateStepErrors(enumerable, dataRow, "AntispamMigrationStep", "AntispamGroupData", "AntispamGroupVisible");
					FFOMigrationStatusService.PopulateStepErrors(enumerable, dataRow, "AntimalwareMigrationStep", "AntimalwareGroupData", "AntimalwareGroupVisible");
					FFOMigrationStatusService.PopulateStepErrors(enumerable, dataRow, "PolicyMigrationStep", "PolicyRulesGroupData", "PolicyRulesGroupVisible");
					FFOMigrationStatusService.PopulateStepErrors(enumerable, dataRow, "ConnectorMigrationStep", "ConnectorGroupData", "ConnectorGroupVisible");
				}
				else
				{
					dataRow["ProblemsMigrating"] = false;
				}
			}
			catch (Exception exception)
			{
				dataRow["ShowError"] = true;
				DDIHelper.Trace("Error processing Migration Report: {0}", new object[]
				{
					exception.GetTraceFormatter()
				});
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0007C138 File Offset: 0x0007A338
		private static void PopulateStepErrors(IEnumerable<FFOMigrationStatusService.MigrationStep> steps, DataRow row, string groupName, string dataField, string visibleField)
		{
			IEnumerable<MigrationReportGroupDetails> source = from s in steps
			where s.StepName == groupName
			select new MigrationReportGroupDetails
			{
				Data = s.LocalizedString
			};
			row[dataField] = source.ToArray<MigrationReportGroupDetails>();
			row[visibleField] = (source.FirstOrDefault<MigrationReportGroupDetails>() != null);
		}

		// Token: 0x02000223 RID: 547
		private class MigrationStep
		{
			// Token: 0x17001C11 RID: 7185
			// (get) Token: 0x0600276E RID: 10094 RVA: 0x0007C1AD File Offset: 0x0007A3AD
			// (set) Token: 0x0600276F RID: 10095 RVA: 0x0007C1B5 File Offset: 0x0007A3B5
			public string StepName { get; private set; }

			// Token: 0x17001C12 RID: 7186
			// (get) Token: 0x06002770 RID: 10096 RVA: 0x0007C1BE File Offset: 0x0007A3BE
			// (set) Token: 0x06002771 RID: 10097 RVA: 0x0007C1C6 File Offset: 0x0007A3C6
			public string ToState { get; private set; }

			// Token: 0x17001C13 RID: 7187
			// (get) Token: 0x06002772 RID: 10098 RVA: 0x0007C1CF File Offset: 0x0007A3CF
			// (set) Token: 0x06002773 RID: 10099 RVA: 0x0007C1D7 File Offset: 0x0007A3D7
			public string LocalizedString { get; private set; }

			// Token: 0x06002774 RID: 10100 RVA: 0x0007C1E0 File Offset: 0x0007A3E0
			public MigrationStep(string stepname, string tostate, string value)
			{
				this.StepName = stepname;
				this.ToState = tostate;
				string serializedString = string.Empty;
				int num;
				if ((num = value.IndexOf("<?xml")) != -1)
				{
					value.IndexOf("</LocalizedString>");
					serializedString = value.Substring(num, value.Length - num - 3);
				}
				LocalizedString localizedString;
				if (LocalizedStringSerializer.TryDeserialize(serializedString, out localizedString))
				{
					this.LocalizedString = localizedString.ToString();
					return;
				}
				this.LocalizedString = string.Empty;
			}
		}
	}
}
