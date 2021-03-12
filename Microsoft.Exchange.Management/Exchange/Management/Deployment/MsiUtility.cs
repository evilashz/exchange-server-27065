using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200029D RID: 669
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MsiUtility
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x00065AD0 File Offset: 0x00063CD0
		public static Guid GetProductCode(string packagePath)
		{
			TaskLogger.LogEnter();
			StringBuilder stringBuilder = new StringBuilder();
			Guid guid = Guid.Empty;
			MsiUtility.PushUILevel(InstallUILevel.None);
			try
			{
				SafeMsiHandle safeMsiHandle;
				uint num = MsiNativeMethods.OpenPackageEx(packagePath, OpenPackageFlags.IgnoreMachineState, out safeMsiHandle);
				if (num != 0U)
				{
					Win32Exception ex = new Win32Exception((int)num);
					throw new TaskException(Strings.MsiCouldNotOpenPackage(packagePath, ex.Message, (int)num), ex);
				}
				using (safeMsiHandle)
				{
					uint num2 = 38U;
					for (;;)
					{
						num2 += 1U;
						if (num2 > 2147483647U)
						{
							break;
						}
						stringBuilder.EnsureCapacity((int)num2);
						num = MsiNativeMethods.GetProductProperty(safeMsiHandle, "ProductCode", stringBuilder, ref num2);
						if (234U != num)
						{
							goto Block_7;
						}
					}
					throw new TaskException(Strings.MsiPropertyTooLarge);
					Block_7:
					if (num != 0U)
					{
						Win32Exception ex2 = new Win32Exception((int)num);
						throw new TaskException(Strings.MsiCouldNotGetProdcutProperty("ProductCode", ex2.Message, (int)num), ex2);
					}
					guid = new Guid(stringBuilder.ToString());
					TaskLogger.Log(Strings.MsiProductCode(guid));
				}
			}
			finally
			{
				MsiUtility.PopUILevel();
			}
			TaskLogger.LogExit();
			return guid;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00065BD8 File Offset: 0x00063DD8
		public static bool IsInstalled(string packagePath)
		{
			TaskLogger.LogEnter();
			if (packagePath == null || packagePath == string.Empty)
			{
				throw new ArgumentNullException("packagePath", Strings.ExceptionProductInfoRequired);
			}
			Guid productCode = MsiUtility.GetProductCode(packagePath);
			bool result = MsiUtility.IsInstalled(productCode);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00065C28 File Offset: 0x00063E28
		public static bool IsInstalled(Guid ProductCode)
		{
			TaskLogger.LogEnter();
			bool result = false;
			if (Guid.Empty == ProductCode)
			{
				throw new ArgumentNullException("ProductCode", Strings.ExceptionProductInfoRequired);
			}
			MsiUtility.PushUILevel(InstallUILevel.None);
			try
			{
				InstallState installState = MsiNativeMethods.QueryProductState(ProductCode.ToString("B").ToUpper(CultureInfo.InvariantCulture));
				result = (installState != InstallState.Unknown);
			}
			finally
			{
				MsiUtility.PopUILevel();
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00065CA8 File Offset: 0x00063EA8
		public static string GetProductInfo(Guid productCode, string propertyName)
		{
			TaskLogger.LogEnter();
			StringBuilder stringBuilder = new StringBuilder();
			string productCodeString = productCode.ToString("B").ToUpper(CultureInfo.InvariantCulture);
			uint num = (uint)stringBuilder.Capacity;
			MsiUtility.PushUILevel(InstallUILevel.None);
			try
			{
				uint productInfo;
				for (;;)
				{
					num += 1U;
					if (num > 2147483647U)
					{
						break;
					}
					stringBuilder.EnsureCapacity((int)num);
					productInfo = MsiNativeMethods.GetProductInfo(productCodeString, propertyName, stringBuilder, ref num);
					if (234U != productInfo)
					{
						goto Block_3;
					}
				}
				throw new TaskException(Strings.MsiPropertyTooLarge);
				Block_3:
				if (productInfo != 0U)
				{
					Win32Exception ex = new Win32Exception((int)productInfo);
					throw new TaskException(Strings.MsiCouldNotGetProdcutProperty(propertyName, ex.Message, (int)productInfo), ex);
				}
			}
			finally
			{
				MsiUtility.PopUILevel();
			}
			TaskLogger.Log(Strings.MsiProperty(propertyName, stringBuilder.ToString()));
			TaskLogger.LogExit();
			return stringBuilder.ToString();
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00065D70 File Offset: 0x00063F70
		private static void PushUILevel(InstallUILevel uiLevel, ref IntPtr window)
		{
			TaskLogger.LogEnter();
			InstallUILevel installUILevel = MsiNativeMethods.SetInternalUI(uiLevel, ref window);
			if (installUILevel == InstallUILevel.NoChange)
			{
				throw new ArgumentOutOfRangeException("uiLevel", installUILevel, Strings.ExceptionInvalidUILevel);
			}
			MsiUtility.uiSettings.Push(new MsiUtility.InternalUISettings(uiLevel, window, null));
			TaskLogger.LogExit();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00065DC4 File Offset: 0x00063FC4
		public static void PushUILevel(InstallUILevel uiLevel)
		{
			TaskLogger.LogEnter();
			IntPtr zero = IntPtr.Zero;
			MsiUtility.PushUILevel(uiLevel, ref zero);
			TaskLogger.LogExit();
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00065DEC File Offset: 0x00063FEC
		public static void PopUILevel()
		{
			TaskLogger.LogEnter();
			MsiUtility.InternalUISettings internalUISettings = (MsiUtility.InternalUISettings)MsiUtility.uiSettings.Pop();
			MsiNativeMethods.SetInternalUI(internalUISettings.UILevel, ref internalUISettings.Window);
			TaskLogger.LogExit();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00065E28 File Offset: 0x00064028
		public static void PushExternalUI(MsiUIHandler handler, InstallLogMode logMode)
		{
			TaskLogger.LogEnter();
			IntPtr zero = IntPtr.Zero;
			InstallUILevel installUILevel = MsiNativeMethods.SetInternalUI(InstallUILevel.None | InstallUILevel.SourceResOnly, ref zero);
			if (installUILevel == InstallUILevel.NoChange)
			{
				throw new ArgumentOutOfRangeException("uiLevel", installUILevel, Strings.ExceptionInvalidUILevel);
			}
			MsiUIHandlerDelegate handlerDelegate = MsiNativeMethods.SetExternalUI(handler.UIHandlerDelegate, logMode, null);
			MsiUtility.uiSettings.Push(new MsiUtility.InternalUISettings(InstallUILevel.None | InstallUILevel.SourceResOnly, zero, handlerDelegate));
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00065E90 File Offset: 0x00064090
		public static void PopExternalUI()
		{
			TaskLogger.LogEnter();
			MsiUtility.InternalUISettings internalUISettings = (MsiUtility.InternalUISettings)MsiUtility.uiSettings.Pop();
			MsiNativeMethods.SetExternalUI(internalUISettings.UIHandlerDelegate, InstallLogMode.None, null);
			MsiNativeMethods.SetInternalUI(internalUISettings.UILevel, ref internalUISettings.Window);
			TaskLogger.LogExit();
		}

		// Token: 0x04000A2C RID: 2604
		private static Stack uiSettings = new Stack();

		// Token: 0x0200029E RID: 670
		private class InternalUISettings
		{
			// Token: 0x06001822 RID: 6178 RVA: 0x00065EE3 File Offset: 0x000640E3
			public InternalUISettings(InstallUILevel uiLevel, IntPtr window, MsiUIHandlerDelegate handlerDelegate)
			{
				this.UILevel = uiLevel;
				this.Window = window;
				this.UIHandlerDelegate = handlerDelegate;
			}

			// Token: 0x04000A2D RID: 2605
			public InstallUILevel UILevel;

			// Token: 0x04000A2E RID: 2606
			public IntPtr Window;

			// Token: 0x04000A2F RID: 2607
			public MsiUIHandlerDelegate UIHandlerDelegate;
		}
	}
}
