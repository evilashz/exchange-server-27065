using System;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200003C RID: 60
	public abstract class Hookable<T>
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00005EF8 File Offset: 0x000040F8
		private Hookable(bool setTestHookNullRestoresDefaultValue, T defaultValue)
		{
			this.setTestHookNullRestoresDefaultValue = setTestHookNullRestoresDefaultValue;
			this.defaultValue = defaultValue;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005F0E File Offset: 0x0000410E
		public static Hookable<T> Create(bool setTestHookNullRestoresDefaultValue, T defaultValue)
		{
			return new Hookable<T>.HookableByReference(setTestHookNullRestoresDefaultValue, defaultValue);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005F17 File Offset: 0x00004117
		public static Hookable<T> Create(bool setTestHookNullRestoresDefaultValue, Func<T> activeValueGetter, Action<T> activeValueSetter)
		{
			return new Hookable<T>.HookableUsingDelegates(setTestHookNullRestoresDefaultValue, activeValueGetter, activeValueSetter);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005F5C File Offset: 0x0000415C
		public static Hookable<T> Create(bool setTestHookNullRestoresDefaultValue, FieldInfo field, object instance)
		{
			return new Hookable<T>.HookableUsingDelegates(setTestHookNullRestoresDefaultValue, () => (T)((object)field.GetValue(instance)), delegate(T value)
			{
				field.SetValue(instance, value);
			});
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005F9C File Offset: 0x0000419C
		public static Hookable<T> Create<TInstance>(bool setTestHookNullRestoresDefaultValue, string fieldName, TInstance instance)
		{
			FieldInfo field = ReflectionHelper.TraverseTypeHierarchy<FieldInfo, string>(typeof(TInstance), fieldName, new MatchType<FieldInfo, string>(ReflectionHelper.MatchInstanceField));
			return Hookable<T>.Create(setTestHookNullRestoresDefaultValue, field, instance);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005FD4 File Offset: 0x000041D4
		public static Hookable<T> Create(bool setTestHookNullRestoresDefaultValue, string fieldName, Type type)
		{
			FieldInfo field = ReflectionHelper.TraverseTypeHierarchy<FieldInfo, string>(type, fieldName, new MatchType<FieldInfo, string>(ReflectionHelper.MatchStaticField));
			return Hookable<T>.Create(setTestHookNullRestoresDefaultValue, field, null);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000160 RID: 352
		// (set) Token: 0x06000161 RID: 353
		public abstract T Value { get; protected set; }

		// Token: 0x06000162 RID: 354 RVA: 0x00006000 File Offset: 0x00004200
		public IDisposable SetTestHook(T testHook)
		{
			IDisposable result = new Hookable<T>.ResetTestHook(this, this.Value);
			this.Value = ((this.setTestHookNullRestoresDefaultValue && testHook == null) ? this.defaultValue : testHook);
			return result;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000603A File Offset: 0x0000423A
		private void UnsetTestHook(T oldValue)
		{
			if (this.setTestHookNullRestoresDefaultValue && oldValue == null)
			{
				this.Value = this.defaultValue;
				return;
			}
			this.Value = oldValue;
		}

		// Token: 0x040000F5 RID: 245
		private readonly T defaultValue;

		// Token: 0x040000F6 RID: 246
		private readonly bool setTestHookNullRestoresDefaultValue;

		// Token: 0x0200003D RID: 61
		private class HookableByReference : Hookable<T>
		{
			// Token: 0x06000164 RID: 356 RVA: 0x00006060 File Offset: 0x00004260
			public HookableByReference(bool setTestHookNullRestoresDefaultValue, T defaultValue) : base(setTestHookNullRestoresDefaultValue, defaultValue)
			{
				this.activeValue = this.defaultValue;
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00006076 File Offset: 0x00004276
			// (set) Token: 0x06000166 RID: 358 RVA: 0x0000607E File Offset: 0x0000427E
			public override T Value
			{
				get
				{
					return this.activeValue;
				}
				protected set
				{
					this.activeValue = value;
				}
			}

			// Token: 0x040000F7 RID: 247
			private T activeValue;
		}

		// Token: 0x0200003E RID: 62
		private class HookableUsingDelegates : Hookable<T>
		{
			// Token: 0x06000167 RID: 359 RVA: 0x00006087 File Offset: 0x00004287
			public HookableUsingDelegates(bool setTestHookNullRestoresDefaultValue, Func<T> activeValueGetter, Action<T> activeValueSetter) : base(setTestHookNullRestoresDefaultValue, activeValueGetter())
			{
				this.activeValueGetter = activeValueGetter;
				this.activeValueSetter = activeValueSetter;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000168 RID: 360 RVA: 0x000060A4 File Offset: 0x000042A4
			// (set) Token: 0x06000169 RID: 361 RVA: 0x000060B1 File Offset: 0x000042B1
			public override T Value
			{
				get
				{
					return this.activeValueGetter();
				}
				protected set
				{
					this.activeValueSetter(value);
				}
			}

			// Token: 0x040000F8 RID: 248
			private readonly Func<T> activeValueGetter;

			// Token: 0x040000F9 RID: 249
			private readonly Action<T> activeValueSetter;
		}

		// Token: 0x0200003F RID: 63
		private class ResetTestHook : IDisposable
		{
			// Token: 0x0600016A RID: 362 RVA: 0x000060BF File Offset: 0x000042BF
			public ResetTestHook(Hookable<T> parent, T oldValue)
			{
				this.disposed = false;
				this.oldValue = oldValue;
				this.parent = parent;
			}

			// Token: 0x0600016B RID: 363 RVA: 0x000060DC File Offset: 0x000042DC
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.parent.UnsetTestHook(this.oldValue);
				}
			}

			// Token: 0x040000FA RID: 250
			private bool disposed;

			// Token: 0x040000FB RID: 251
			private T oldValue;

			// Token: 0x040000FC RID: 252
			private Hookable<T> parent;
		}
	}
}
