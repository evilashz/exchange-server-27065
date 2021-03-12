using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000312 RID: 786
	[Serializable]
	public class ActiveSyncDeviceFilterRule : XMLSerializableBase
	{
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x0009B46E File Offset: 0x0009966E
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x0009B476 File Offset: 0x00099676
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x0009B47F File Offset: 0x0009967F
		// (set) Token: 0x06002439 RID: 9273 RVA: 0x0009B487 File Offset: 0x00099687
		[XmlAttribute(AttributeName = "characteristic")]
		public DeviceAccessCharacteristic Characteristic { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x0009B490 File Offset: 0x00099690
		// (set) Token: 0x0600243B RID: 9275 RVA: 0x0009B498 File Offset: 0x00099698
		[XmlAttribute(AttributeName = "operator")]
		public DeviceFilterOperator Operator { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x0009B4A1 File Offset: 0x000996A1
		// (set) Token: 0x0600243D RID: 9277 RVA: 0x0009B4A9 File Offset: 0x000996A9
		[XmlText]
		public string Value { get; set; }

		// Token: 0x0600243E RID: 9278 RVA: 0x0009B4B2 File Offset: 0x000996B2
		public ActiveSyncDeviceFilterRule() : this(null, DeviceAccessCharacteristic.DeviceType, DeviceFilterOperator.Equals, null)
		{
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0009B4BE File Offset: 0x000996BE
		public ActiveSyncDeviceFilterRule(string name, DeviceAccessCharacteristic charactersitic, DeviceFilterOperator filterOperator, string value)
		{
			this.Name = name;
			this.Characteristic = charactersitic;
			this.Operator = filterOperator;
			this.Value = value;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0009B4E3 File Offset: 0x000996E3
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Characteristic.GetHashCode() ^ this.Operator.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0009B520 File Offset: 0x00099720
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ActiveSyncDeviceFilterRule activeSyncDeviceFilterRule = obj as ActiveSyncDeviceFilterRule;
			return activeSyncDeviceFilterRule != null && (string.Equals(this.Name, activeSyncDeviceFilterRule.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Value, activeSyncDeviceFilterRule.Value, StringComparison.OrdinalIgnoreCase) && this.Characteristic == activeSyncDeviceFilterRule.Characteristic) && this.Operator == activeSyncDeviceFilterRule.Operator;
		}
	}
}
