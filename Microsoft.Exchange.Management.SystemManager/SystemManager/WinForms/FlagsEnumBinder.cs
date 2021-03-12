using System;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000111 RID: 273
	public class FlagsEnumBinder<T> where T : struct
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x00022936 File Offset: 0x00020B36
		public FlagsEnumBinder(object dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00022A18 File Offset: 0x00020C18
		public void BindCheckBoxToFlag(AutoHeightCheckBox checkBox, T flag)
		{
			if (checkBox == null)
			{
				throw new ArgumentNullException("checkBox");
			}
			ulong flagValue = Convert.ToUInt64(flag);
			Binding binding = checkBox.DataBindings.Add("Checked", this.dataSource, this.dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
			binding.Format += delegate(object sender, ConvertEventArgs e)
			{
				this.currentEnumValue = ((e.Value == null) ? 0UL : Convert.ToUInt64(e.Value));
				e.Value = ((this.currentEnumValue & flagValue) == flagValue);
			};
			binding.Parse += delegate(object sender, ConvertEventArgs e)
			{
				if (true.Equals(e.Value))
				{
					e.Value = Enum.ToObject(typeof(T), this.currentEnumValue | flagValue);
					return;
				}
				e.Value = Enum.ToObject(typeof(T), this.currentEnumValue & ~flagValue);
			};
		}

		// Token: 0x04000450 RID: 1104
		private ulong currentEnumValue;

		// Token: 0x04000451 RID: 1105
		private object dataSource;

		// Token: 0x04000452 RID: 1106
		private string dataMember;
	}
}
