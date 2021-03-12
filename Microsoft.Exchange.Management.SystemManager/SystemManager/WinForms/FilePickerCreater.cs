using System;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022A RID: 554
	public static class FilePickerCreater
	{
		// Token: 0x060019A8 RID: 6568 RVA: 0x0006F4A4 File Offset: 0x0006D6A4
		public static FilePicker CreateAudioFilePicker()
		{
			return new OpenFilePicker
			{
				Filter = string.Format("{0}(*.wav)|*.wav|{1}(*.wma)|*.wma", Strings.WavFileDescription, Strings.WmaFileDescription),
				DefaultExt = "wav"
			};
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0006F4E8 File Offset: 0x0006D6E8
		public static FilePicker CreateAppFilePicker()
		{
			return new OpenFilePicker
			{
				Filter = string.Format("{0}(*.dll, *.exe, *.cab)|*.dll;*.exe;*.cab", Strings.AppFileDescription)
			};
		}
	}
}
