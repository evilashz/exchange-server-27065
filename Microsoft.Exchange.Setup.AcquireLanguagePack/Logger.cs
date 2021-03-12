using System;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000005 RID: 5
	internal class Logger
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020FD File Offset: 0x000002FD
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		public static bool ErrorFound
		{
			get
			{
				return Logger.errorFound;
			}
			set
			{
				Logger.errorFound = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		private static void NewLogSetup()
		{
			Logger.isNewLog = false;
			if (!Directory.Exists(Logger.PathToDirLog))
			{
				Directory.CreateDirectory(Logger.PathToDirLog);
			}
			StreamWriter streamWriter = new StreamWriter(Logger.PathToFileLog, true);
			streamWriter.WriteLine("\n\n ************************\n");
			streamWriter.Close();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public static void LoggerMessage(string message)
		{
			lock (Logger.objLock)
			{
				if (Logger.isNewLog)
				{
					Logger.NewLogSetup();
				}
				StreamWriter streamWriter = new StreamWriter(Logger.PathToFileLog, true);
				streamWriter.WriteLine("-- " + message);
				streamWriter.Close();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BC File Offset: 0x000003BC
		public static void LogError(Exception exception)
		{
			lock (Logger.objLock)
			{
				Logger.errorFound = true;
				if (Logger.isNewLog)
				{
					Logger.NewLogSetup();
				}
				StreamWriter streamWriter = new StreamWriter(Logger.PathToFileLog, true);
				streamWriter.WriteLine(Strings.ErrorInLog + " -- " + exception.ToString());
				streamWriter.Close();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000223C File Offset: 0x0000043C
		public static void UserCancel()
		{
			Logger.errorFound = true;
		}

		// Token: 0x04000001 RID: 1
		public static readonly string PathToDirLog = Path.Combine(Path.GetPathRoot(Environment.GetEnvironmentVariable("windir")), "ExchangeSetupLogs\\");

		// Token: 0x04000002 RID: 2
		public static readonly string PathToFileLog = Path.Combine(Logger.PathToDirLog, "LPSetupUILog.log");

		// Token: 0x04000003 RID: 3
		private static bool isNewLog = true;

		// Token: 0x04000004 RID: 4
		private static bool errorFound = false;

		// Token: 0x04000005 RID: 5
		private static object objLock = new object();
	}
}
