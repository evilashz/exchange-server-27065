using System;
using System.Reflection;
using System.Threading;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200002B RID: 43
	public class ScriptEntry
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005D38 File Offset: 0x00003F38
		public ScriptEntry() : this(null, null, false)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005D43 File Offset: 0x00003F43
		public ScriptEntry(string assembly, string name, bool skipScriptResources)
		{
			this.Assembly = assembly;
			this.Name = name;
			this.SkipScriptResources = skipScriptResources;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005D60 File Offset: 0x00003F60
		public ScriptEntry(ScriptReference scriptReference, bool skipScriptResources) : this(scriptReference.Assembly, scriptReference.Name, skipScriptResources)
		{
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005D75 File Offset: 0x00003F75
		public ScriptEntry(ScriptReference scriptReference) : this(scriptReference.Assembly, scriptReference.Name, false)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005D8A File Offset: 0x00003F8A
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00005D92 File Offset: 0x00003F92
		public string Assembly { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005D9B File Offset: 0x00003F9B
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00005DA3 File Offset: 0x00003FA3
		public string Name { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005DAC File Offset: 0x00003FAC
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public bool SkipScriptResources { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public bool HasScriptResources
		{
			get
			{
				foreach (ScriptResourceAttribute scriptResourceAttribute in this.LoadAssembly().GetCustomAttributes(typeof(ScriptResourceAttribute), false))
				{
					if (scriptResourceAttribute.ScriptName == this.Name)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E18 File Offset: 0x00004018
		public Assembly LoadAssembly()
		{
			if (null == this.loadedAssembly)
			{
				Assembly value = System.Reflection.Assembly.Load(this.Assembly);
				Interlocked.CompareExchange<Assembly>(ref this.loadedAssembly, value, null);
			}
			return this.loadedAssembly;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005E54 File Offset: 0x00004054
		public override bool Equals(object obj)
		{
			ScriptEntry scriptEntry = (ScriptEntry)obj;
			return scriptEntry.Assembly == this.Assembly && scriptEntry.Name == this.Name;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005E8E File Offset: 0x0000408E
		public override int GetHashCode()
		{
			return this.Assembly.GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x0400005D RID: 93
		private Assembly loadedAssembly;
	}
}
