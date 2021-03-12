using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000435 RID: 1077
	[DataContract]
	internal class EnumParameter : FormletParameter
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x000A7284 File Offset: 0x000A5484
		public EnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, string defaultVal, bool useLocalizedDescription) : this(name, dialogTitle, dialogLabel, defaultVal)
		{
			if (objectType != null)
			{
				Array values = Enum.GetValues(objectType);
				EnumValue[] array = new EnumValue[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = (useLocalizedDescription ? new EnumValue((Enum)values.GetValue(i), true) : new EnumValue((Enum)values.GetValue(i)));
				}
				this.Values = array;
			}
			else
			{
				this.Values = null;
			}
			this.IsFlags = !objectType.GetCustomAttributes(typeof(FlagsAttribute), false).IsNullOrEmpty();
			base.FormletType = typeof(EnumComboBoxModalEditor);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000A7337 File Offset: 0x000A5537
		public EnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, string defaultVal) : this(name, dialogTitle, dialogLabel, objectType, defaultVal, false)
		{
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000A7347 File Offset: 0x000A5547
		public EnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, string defaultVal, bool useLocalizedDescription, LocalizedString noSelectionText) : this(name, dialogTitle, dialogLabel, objectType, defaultVal, false)
		{
			this.noSelectionText = noSelectionText;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000A735F File Offset: 0x000A555F
		public EnumParameter(string name, LocalizedString displayName, LocalizedString description, string defaultVal) : base(name, displayName, description)
		{
			this.defaultValue = ((defaultVal != null) ? new EnumValue(defaultVal, defaultVal) : null);
			base.FormletType = typeof(EnumComboBoxModalEditor);
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000A7390 File Offset: 0x000A5590
		// (set) Token: 0x060035E3 RID: 13795 RVA: 0x000A73C6 File Offset: 0x000A55C6
		[DataMember]
		public EnumValue DefaultValue
		{
			get
			{
				if (this.defaultValue != null)
				{
					return this.defaultValue;
				}
				if (this.Values != null && this.Values.Length > 0 && !this.IsFlags)
				{
					return this.Values[0];
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x000A73CD File Offset: 0x000A55CD
		// (set) Token: 0x060035E5 RID: 13797 RVA: 0x000A73D5 File Offset: 0x000A55D5
		[DataMember]
		public EnumValue[] Values { get; internal set; }

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x060035E6 RID: 13798 RVA: 0x000A73DE File Offset: 0x000A55DE
		// (set) Token: 0x060035E7 RID: 13799 RVA: 0x000A73E6 File Offset: 0x000A55E6
		[DataMember]
		public bool IsFlags { get; private set; }

		// Token: 0x040025B7 RID: 9655
		private EnumValue defaultValue;
	}
}
