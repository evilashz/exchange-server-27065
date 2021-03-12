using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class InvokeNowEntry : IPersistence
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0005B01D File Offset: 0x0005921D
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0005B025 File Offset: 0x00059225
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0005B02E File Offset: 0x0005922E
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0005B036 File Offset: 0x00059236
		public string TypeName { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0005B03F File Offset: 0x0005923F
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0005B047 File Offset: 0x00059247
		public string AssemblyPath { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0005B050 File Offset: 0x00059250
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0005B058 File Offset: 0x00059258
		public string MonitorIdentity { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0005B061 File Offset: 0x00059261
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0005B069 File Offset: 0x00059269
		public Guid Id { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0005B072 File Offset: 0x00059272
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0005B07A File Offset: 0x0005927A
		public string PropertyBag { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0005B083 File Offset: 0x00059283
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0005B08B File Offset: 0x0005928B
		public string ExtensionAttributes { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0005B094 File Offset: 0x00059294
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0005B09C File Offset: 0x0005929C
		public DateTime RequestTime { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0005B0A5 File Offset: 0x000592A5
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x0005B0AD File Offset: 0x000592AD
		public InvokeNowState State { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0005B0B6 File Offset: 0x000592B6
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0005B0BE File Offset: 0x000592BE
		public InvokeNowResult Result { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0005B0C7 File Offset: 0x000592C7
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x0005B0CF File Offset: 0x000592CF
		public string ErrorMessage { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0005B0D8 File Offset: 0x000592D8
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0005B0E0 File Offset: 0x000592E0
		public string WorkDefinitionId { get; set; }

		// Token: 0x06000D6B RID: 3435 RVA: 0x0005B0E9 File Offset: 0x000592E9
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0005B0FC File Offset: 0x000592FC
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			this.Id = Guid.Parse(this.GetStringProperty(propertyBag, "Id"));
			this.TypeName = this.GetStringProperty(propertyBag, "TypeName");
			this.AssemblyPath = this.GetStringProperty(propertyBag, "AssemblyPath");
			this.MonitorIdentity = this.GetStringProperty(propertyBag, "MonitorIdentity");
			this.PropertyBag = this.GetStringProperty(propertyBag, "PropertyBag");
			this.ExtensionAttributes = this.GetStringProperty(propertyBag, "ExtensionAttributes");
			this.RequestTime = this.GetDateTimeProperty(propertyBag, "RequestTime");
			this.State = this.GetEnumProperty<InvokeNowState>(propertyBag, "State");
			this.Result = this.GetEnumProperty<InvokeNowResult>(propertyBag, "Result");
			this.ErrorMessage = this.GetStringProperty(propertyBag, "ErrorMessage");
			this.WorkDefinitionId = this.GetStringProperty(propertyBag, "WorkDefinitionId");
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0005B1D4 File Offset: 0x000593D4
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			this.GetCrimsonEventToUse();
			ManagedAvailabilityCrimsonEvents.InvokeNowRequest.Log<Guid, string, string, string, string, string, string, string, string, string, string>(this.Id, this.TypeName, this.AssemblyPath, this.MonitorIdentity, this.PropertyBag, this.ExtensionAttributes, this.ToUniversalSortableTimeString(this.RequestTime), this.State.ToString(), this.RequestTime.ToString(), this.ErrorMessage, this.WorkDefinitionId);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0005B254 File Offset: 0x00059454
		private ManagedAvailabilityCrimsonEvent GetCrimsonEventToUse()
		{
			ManagedAvailabilityCrimsonEvent result = null;
			if (this.State == InvokeNowState.DefinitionUploadStarted)
			{
				result = ManagedAvailabilityCrimsonEvents.InvokeNowDefinitionUploadStarted;
			}
			if (this.State == InvokeNowState.DefinitionUploadFinished)
			{
				if (this.Result == InvokeNowResult.Succeeded)
				{
					result = ManagedAvailabilityCrimsonEvents.InvokeNowDefinitionUploadSucceeded;
				}
				else
				{
					result = ManagedAvailabilityCrimsonEvents.InvokeNowDefinitionUploadFailed;
				}
			}
			if (this.State == InvokeNowState.MonitorInvokeFinished)
			{
				if (this.Result == InvokeNowResult.Succeeded)
				{
					result = ManagedAvailabilityCrimsonEvents.InvokeNowSucceeded;
				}
				else
				{
					result = ManagedAvailabilityCrimsonEvents.InvokeNowFailed;
				}
			}
			return result;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0005B2B4 File Offset: 0x000594B4
		private string ToUniversalSortableTimeString(DateTime dateTime)
		{
			return dateTime.ToUniversalTime().ToString("o");
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0005B2D8 File Offset: 0x000594D8
		private string GetStringProperty(Dictionary<string, string> propertyBag, string propertyName)
		{
			string text = null;
			if (propertyBag.TryGetValue(propertyName, out text))
			{
				text = CrimsonHelper.NullDecode(text);
			}
			return text;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0005B2FC File Offset: 0x000594FC
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

		// Token: 0x06000D72 RID: 3442 RVA: 0x0005B32C File Offset: 0x0005952C
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
	}
}
