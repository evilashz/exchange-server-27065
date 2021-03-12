using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C4 RID: 1220
	[KnownType(typeof(NewUMMailboxParameters))]
	[DataContract]
	[KnownType(typeof(SetUMMailboxPinParameters))]
	public abstract class UMBasePinSetParameteres : SetObjectProperties
	{
		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x000B4ABF File Offset: 0x000B2CBF
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x000B4AC7 File Offset: 0x000B2CC7
		[DataMember]
		public string AutoPin { get; set; }

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000B4AD0 File Offset: 0x000B2CD0
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x000B4AD8 File Offset: 0x000B2CD8
		[DataMember]
		public string ManualPin { get; set; }

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x000B4AE1 File Offset: 0x000B2CE1
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x000B4AF3 File Offset: 0x000B2CF3
		[DataMember]
		public bool? PinExpired
		{
			get
			{
				return (bool?)base["PinExpired"];
			}
			set
			{
				base["PinExpired"] = value;
			}
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x000B4B08 File Offset: 0x000B2D08
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			bool flag;
			if (bool.TryParse(this.AutoPin, out flag))
			{
				if (flag)
				{
					base["Pin"] = null;
					return;
				}
				this.ManualPin.FaultIfNullOrEmpty(Strings.UMManualPin);
				base["Pin"] = this.ManualPin;
			}
		}
	}
}
