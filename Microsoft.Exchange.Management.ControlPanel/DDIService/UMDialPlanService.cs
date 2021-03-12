using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004D3 RID: 1235
	public class UMDialPlanService : DDICodeBehind
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x000B590C File Offset: 0x000B3B0C
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			bool flag = !DDIHelper.IsEmptyValue(inputRow["NoSipDialPlans"]) && (bool)inputRow["NoSipDialPlans"];
			Identity identity = inputRow["NoSipDialPlansExcludeId"] as Identity;
			if (flag)
			{
				List<DataRow> list = new List<DataRow>();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if ((UMUriType)dataRow["URIType"] == UMUriType.SipName && !((ADObjectId)dataRow["Identity"]).ToIdentity().Equals(identity))
					{
						list.Add(dataRow);
					}
				}
				list.ForEach(delegate(DataRow x)
				{
					x.Delete();
				});
			}
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000B5A48 File Offset: 0x000B3C48
		public static List<DropDownItemData> GetAvailableUmLanguages()
		{
			MultiValuedProperty<UMLanguage> multiValuedProperty = Utils.ComputeUnionOfUmServerLanguages();
			List<DropDownItemData> list = new List<DropDownItemData>(multiValuedProperty.Count);
			bool isRtl = RtlUtil.IsRtl;
			list.AddRange(multiValuedProperty.ConvertAll((UMLanguage x) => new DropDownItemData(RtlUtil.ConvertToDecodedBidiString(x.Culture.NativeName, isRtl), x.Culture.LCID.ToString())));
			return list;
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x000B5A91 File Offset: 0x000B3C91
		public static object GetInfoAnnouncementEnabled(string infoAnnouncementFilename, bool isInfoAnnouncementInterruptible)
		{
			return string.IsNullOrEmpty(infoAnnouncementFilename) ? InfoAnnouncementEnabledEnum.False : (isInfoAnnouncementInterruptible ? InfoAnnouncementEnabledEnum.True : InfoAnnouncementEnabledEnum.Uninterruptible);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x000B5AB2 File Offset: 0x000B3CB2
		public static string[] UMDialingRuleEntryToTask(object value)
		{
			if (!DDIHelper.IsEmptyValue(value))
			{
				return UMDialingRuleEntry.GetStringArray(Array.ConvertAll<object, UMDialingRuleEntry>((object[])value, (object x) => (UMDialingRuleEntry)x));
			}
			return null;
		}
	}
}
