using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.Dsn
{
	// Token: 0x02000092 RID: 146
	[Cmdlet("Get", "SystemMessage", DefaultParameterSetName = "Identity")]
	public sealed class GetSystemMessage : GetSystemConfigurationObjectTask<SystemMessageIdParameter, SystemMessage>
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00014445 File Offset: 0x00012645
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001446B File Offset: 0x0001266B
		[Parameter(Mandatory = false, ParameterSetName = "Original")]
		public SwitchParameter Original
		{
			get
			{
				return (SwitchParameter)(base.Fields["Original"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Original"] = value;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00014484 File Offset: 0x00012684
		private static SystemMessage[] GetAllResourceSystemMessages(ADObjectId orgContainer)
		{
			CultureInfo[] allSupportedDsnLanguages = ClientCultures.GetAllSupportedDsnLanguages();
			string[] array;
			LocalizedString[] array2;
			DsnDefaultMessages.GetAllDsnCodesAndMessages(out array, out array2);
			QuotaMessageType[] array3 = (QuotaMessageType[])Enum.GetValues(typeof(QuotaMessageType));
			SystemMessage[] array4 = new SystemMessage[(array.Length + array3.Length) * allSupportedDsnLanguages.Length];
			int num = 0;
			foreach (CultureInfo cultureInfo in allSupportedDsnLanguages)
			{
				ADObjectId dsnCustomizationContainer = SystemMessage.GetDsnCustomizationContainer(orgContainer);
				ADObjectId childId = dsnCustomizationContainer.GetChildId(cultureInfo.LCID.ToString(NumberFormatInfo.InvariantInfo));
				ADObjectId childId2 = childId.GetChildId("internal");
				for (int j = 0; j < array.Length; j++)
				{
					array4[num] = new SystemMessage();
					array4[num].SetId(childId2.GetChildId(array[j]));
					array4[num].Text = array2[j].ToString(cultureInfo);
					num++;
				}
				foreach (QuotaMessageType quotaMessageType in array3)
				{
					QuotaLocalizedTexts quotaLocalizedTexts = QuotaLocalizedTexts.GetQuotaLocalizedTexts(SetSystemMessage.ConvertToInternal(quotaMessageType), string.Empty, string.Empty, true);
					array4[num] = new SystemMessage();
					array4[num].SetId(childId.GetChildId(quotaMessageType.ToString()));
					array4[num].Text = quotaLocalizedTexts.Details.ToString(cultureInfo);
					num++;
				}
			}
			return array4;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000145F2 File Offset: 0x000127F2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000145F8 File Offset: 0x000127F8
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId currentOrgContainerId = base.CurrentOrgContainerId;
				return SystemMessage.GetDsnCustomizationContainer(currentOrgContainerId);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00014614 File Offset: 0x00012814
		protected override void InternalProcessRecord()
		{
			if (this.Original)
			{
				ADObjectId orgContainerId = (base.DataSession as IConfigurationSession).GetOrgContainerId();
				this.WriteResult<SystemMessage>(GetSystemMessage.GetAllResourceSystemMessages(orgContainerId));
				return;
			}
			base.InternalProcessRecord();
		}
	}
}
