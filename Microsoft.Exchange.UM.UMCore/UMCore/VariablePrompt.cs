using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000003 RID: 3
	internal abstract class VariablePrompt<T> : Prompt
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021FC File Offset: 0x000003FC
		public VariablePrompt()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000221C File Offset: 0x0000041C
		internal VariablePrompt(string promptName, CultureInfo culture, T value)
		{
			PromptSetting config = new PromptSetting(promptName);
			this.Initialize(config, culture, value);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002256 File Offset: 0x00000456
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000225E File Offset: 0x0000045E
		protected T InitVal
		{
			get
			{
				return this.initVal;
			}
			set
			{
				this.initVal = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000226F File Offset: 0x0000046F
		protected StringBuilder SbSsml
		{
			get
			{
				return this.sbSsml;
			}
			set
			{
				this.sbSsml = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002278 File Offset: 0x00000478
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002280 File Offset: 0x00000480
		protected StringBuilder SbLog
		{
			get
			{
				return this.sbLog;
			}
			set
			{
				this.sbLog = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002289 File Offset: 0x00000489
		protected virtual PromptConfigBase PreviewConfig
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
		internal static void SetActualPromptValues(ArrayList prompts, string varName, T varValue)
		{
			foreach (object obj in prompts)
			{
				Prompt prompt = (Prompt)obj;
				VariablePrompt<T> variablePrompt = prompt as VariablePrompt<T>;
				if (variablePrompt != null && string.Equals(variablePrompt.PromptName, varName, StringComparison.OrdinalIgnoreCase))
				{
					variablePrompt.SetInitVal(varValue);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002300 File Offset: 0x00000500
		internal static ArrayList GetPreview<P>(P prompt, CultureInfo c, T initVal) where P : VariablePrompt<T>
		{
			ArrayList arrayList = new ArrayList();
			PromptSetting config = new PromptSetting(prompt.PreviewConfig);
			prompt.Initialize(config, c, initVal);
			arrayList.Add(prompt);
			return arrayList;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002345 File Offset: 0x00000545
		internal virtual void SetInitVal(T initVal)
		{
			this.initVal = initVal;
			this.InternalInitialize();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002354 File Offset: 0x00000554
		internal virtual void SetInitValOnly(T promptValue)
		{
			this.initVal = promptValue;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000235D File Offset: 0x0000055D
		internal void Initialize(PromptSetting config, CultureInfo c, T initVal)
		{
			this.initVal = initVal;
			this.SbLog.AppendLine();
			if (!base.IsInitialized)
			{
				base.Initialize(config, c);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002382 File Offset: 0x00000582
		protected virtual string AddProsodyWithVolume(string text)
		{
			return Util.AddProsodyWithVolume(base.Culture, text);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002390 File Offset: 0x00000590
		protected void AddPrompts(ArrayList prompts)
		{
			foreach (object obj in prompts)
			{
				Prompt p = (Prompt)obj;
				this.AddPrompt(p);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E4 File Offset: 0x000005E4
		protected void AddPrompt(Prompt p)
		{
			this.SbSsml.Append(p.ToSsml());
			this.SbLog.AppendLine();
			this.SbLog.Append(p.ToString());
		}

		// Token: 0x04000007 RID: 7
		private StringBuilder sbSsml = new StringBuilder();

		// Token: 0x04000008 RID: 8
		private StringBuilder sbLog = new StringBuilder();

		// Token: 0x04000009 RID: 9
		private T initVal;
	}
}
