using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000131 RID: 305
	public static class HpAcuCliWrapper
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x000344A8 File Offset: 0x000326A8
		private static string InvokeHpAcuCli(string command)
		{
			foreach (string processName in HpAcuCliWrapper.HpAcuProcesses)
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				if (processesByName != null && processesByName.Length > 0)
				{
					foreach (Process process in processesByName)
					{
						using (process)
						{
							process.Kill();
						}
					}
				}
			}
			if (string.IsNullOrEmpty(HpAcuCliWrapper.HpAcuCliProcessLocation))
			{
				HpAcuCliWrapper.HpAcuCliProcessLocation = string.Format("{0}\\Compaq\\Hpacucli\\bin\\hpacucli.exe", "C:\\Program Files");
				if (!File.Exists(HpAcuCliWrapper.HpAcuCliProcessLocation))
				{
					HpAcuCliWrapper.HpAcuCliProcessLocation = string.Format("{0}\\Compaq\\Hpacucli\\bin\\hpacucli.exe", "C:\\Program Files (x86)");
				}
			}
			string result = string.Empty;
			string text = string.Empty;
			using (Process process3 = new Process())
			{
				process3.StartInfo.FileName = HpAcuCliWrapper.HpAcuCliProcessLocation;
				process3.StartInfo.Arguments = command;
				process3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process3.StartInfo.CreateNoWindow = true;
				process3.StartInfo.RedirectStandardOutput = true;
				process3.StartInfo.RedirectStandardError = true;
				process3.StartInfo.UseShellExecute = false;
				process3.Start();
				result = process3.StandardOutput.ReadToEnd();
				text = process3.StandardError.ReadToEnd();
				process3.WaitForExit(30000);
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				throw new ApplicationException(string.Format("HpAcuCli returned error - {0}", text));
			}
			return result;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00034644 File Offset: 0x00032844
		public static HpAcuCliWrapper.ControllerStatusSimple[] ListAllControllers()
		{
			return HpAcuCliWrapper.ControllerStatusSimple.ConvertFromRawString(HpAcuCliWrapper.InvokeHpAcuCli("controller all show status"));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00034655 File Offset: 0x00032855
		public static HpAcuCliWrapper.ModifyCommandResult ModifyLogicalDriveCaching(int slotNumber, bool enable)
		{
			return new HpAcuCliWrapper.ModifyCommandResult(HpAcuCliWrapper.InvokeHpAcuCli(string.Format("controller slot={0} logicaldrive all modify caching={1}", slotNumber, enable ? "enable" : "disable")));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00034680 File Offset: 0x00032880
		public static HpAcuCliWrapper.ModifyCommandResult ModifyLogicalDriveArrayAccelerator(int slotNumber, bool enable)
		{
			return new HpAcuCliWrapper.ModifyCommandResult(HpAcuCliWrapper.InvokeHpAcuCli(string.Format("controller slot={0} logicaldrive all modify arrayaccelerator={1}", slotNumber, enable ? "enable" : "disable")));
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000346AB File Offset: 0x000328AB
		public static HpAcuCliWrapper.ModifyCommandResult ResetNoBatteryWriteCache(int slotNumber)
		{
			return new HpAcuCliWrapper.ModifyCommandResult(HpAcuCliWrapper.InvokeHpAcuCli(string.Format("controller slot={0} modify nobatterywritecache=disable", slotNumber)));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000346C7 File Offset: 0x000328C7
		public static HpAcuCliWrapper.ModifyCommandResult ResetCacheRatio(int slotNumber)
		{
			return new HpAcuCliWrapper.ModifyCommandResult(HpAcuCliWrapper.InvokeHpAcuCli(string.Format("controller slot={0} modify cacheratio=0/100", slotNumber)));
		}

		// Token: 0x04000618 RID: 1560
		private const string ListAllControllersCmd = "controller all show status";

		// Token: 0x04000619 RID: 1561
		private const string ModifyLogicalDriveCachingCmd = "controller slot={0} logicaldrive all modify caching={1}";

		// Token: 0x0400061A RID: 1562
		private const string ModifyLogicalDriveArrayAcceleratorCmd = "controller slot={0} logicaldrive all modify arrayaccelerator={1}";

		// Token: 0x0400061B RID: 1563
		private const string ModifyCacheRatioCmd = "controller slot={0} modify cacheratio=0/100";

		// Token: 0x0400061C RID: 1564
		private const string SetNoBatteryWriteCacheCmd = "controller slot={0} modify nobatterywritecache=disable";

		// Token: 0x0400061D RID: 1565
		private const string HpAcuCliLocationPattern = "{0}\\Compaq\\Hpacucli\\bin\\hpacucli.exe";

		// Token: 0x0400061E RID: 1566
		private static readonly string[] HpAcuProcesses = new string[]
		{
			"hpacuhost",
			"hpacucli"
		};

		// Token: 0x0400061F RID: 1567
		private static string HpAcuCliProcessLocation = string.Empty;

		// Token: 0x02000132 RID: 306
		public class ControllerStatusSimple
		{
			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060008FE RID: 2302 RVA: 0x00034718 File Offset: 0x00032918
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060008FF RID: 2303 RVA: 0x00034720 File Offset: 0x00032920
			public int SlotNumber
			{
				get
				{
					return this.slotNumber;
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000900 RID: 2304 RVA: 0x00034728 File Offset: 0x00032928
			public string Status
			{
				get
				{
					return this.status;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000901 RID: 2305 RVA: 0x00034730 File Offset: 0x00032930
			public string CacheStatus
			{
				get
				{
					return this.cacheStatus;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000902 RID: 2306 RVA: 0x00034738 File Offset: 0x00032938
			public string BatteryStatus
			{
				get
				{
					return this.batteryStatus;
				}
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x0003477C File Offset: 0x0003297C
			public static HpAcuCliWrapper.ControllerStatusSimple[] ConvertFromRawString(string rawOutput)
			{
				if (string.IsNullOrWhiteSpace(rawOutput) && !rawOutput.StartsWith("Error"))
				{
					return null;
				}
				string[] array = rawOutput.Split(HpAcuCliWrapper.ControllerStatusSimple.splitList, StringSplitOptions.None);
				HpAcuCliWrapper.ControllerStatusSimple controllerStatusSimple = null;
				List<HpAcuCliWrapper.ControllerStatusSimple> list = new List<HpAcuCliWrapper.ControllerStatusSimple>();
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrWhiteSpace(array[i]))
					{
						if (!array[i].StartsWith(" "))
						{
							if (controllerStatusSimple != null)
							{
								list.Add(controllerStatusSimple);
							}
							controllerStatusSimple = new HpAcuCliWrapper.ControllerStatusSimple();
							string[] array2 = array[i].Split(new string[]
							{
								" in "
							}, StringSplitOptions.None);
							if (array2.Length >= 2)
							{
								controllerStatusSimple.name = array2[0].Trim();
								Match match = HpAcuCliWrapper.ControllerStatusSimple.slotMatch.Match(array2[1]);
								if (match.Success)
								{
									string value = match.Groups["slotnumber"].Value;
									int num = int.Parse(value);
									controllerStatusSimple.slotNumber = num;
								}
							}
						}
						else if (controllerStatusSimple != null)
						{
							string[] array3 = array[i].Split(new char[]
							{
								':'
							});
							if (array3 != null && array3.Length >= 2)
							{
								string text = array3[0].Trim();
								string text2 = array3[1].Trim();
								if (text.Equals("Controller Status", StringComparison.OrdinalIgnoreCase))
								{
									controllerStatusSimple.status = text2;
								}
								else if (text.Equals("Cache Status", StringComparison.OrdinalIgnoreCase))
								{
									controllerStatusSimple.cacheStatus = text2;
								}
								else if (text.Equals("Battery/Capacitor Status", StringComparison.OrdinalIgnoreCase))
								{
									controllerStatusSimple.batteryStatus = text2;
								}
							}
						}
					}
				}
				if (controllerStatusSimple != null)
				{
					list.Add(controllerStatusSimple);
				}
				return list.ToArray();
			}

			// Token: 0x04000620 RID: 1568
			private static readonly string[] splitList = new string[]
			{
				"\r\n",
				"\n"
			};

			// Token: 0x04000621 RID: 1569
			private static Regex slotMatch = new Regex("Slot\\ (?<slotnumber>[0-9]+)", RegexOptions.Compiled);

			// Token: 0x04000622 RID: 1570
			private string name = string.Empty;

			// Token: 0x04000623 RID: 1571
			private int slotNumber = -1;

			// Token: 0x04000624 RID: 1572
			private string status = string.Empty;

			// Token: 0x04000625 RID: 1573
			private string cacheStatus = string.Empty;

			// Token: 0x04000626 RID: 1574
			private string batteryStatus = string.Empty;
		}

		// Token: 0x02000133 RID: 307
		public class ModifyCommandResult
		{
			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000906 RID: 2310 RVA: 0x0003494E File Offset: 0x00032B4E
			public bool Success
			{
				get
				{
					return this.success;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000907 RID: 2311 RVA: 0x00034956 File Offset: 0x00032B56
			public string ErrorMessage
			{
				get
				{
					return this.errorMessage;
				}
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x0003495E File Offset: 0x00032B5E
			public ModifyCommandResult(string rawOutput)
			{
				if (string.IsNullOrWhiteSpace(rawOutput))
				{
					this.success = true;
					this.errorMessage = string.Empty;
					return;
				}
				this.success = false;
				this.errorMessage = rawOutput;
			}

			// Token: 0x04000627 RID: 1575
			private readonly bool success;

			// Token: 0x04000628 RID: 1576
			private readonly string errorMessage;
		}
	}
}
