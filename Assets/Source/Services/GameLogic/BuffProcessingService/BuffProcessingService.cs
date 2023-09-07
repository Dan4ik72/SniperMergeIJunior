﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public class BuffProcessingService
{
    private List<IBuffable> _allBuffables;

    public async void ApplyBuff<T>(T buff) where T : Buff
    {
        var buffable = _allBuffables.FirstOrDefault(buffable => buffable.BuffableType == typeof(T));

        if (buffable == null)
            throw new InvalidCastException("There is no buffable with type " + typeof(T) + " in the list");
        
        buffable.ApplyBuff(buff);
        await WaitForBuffEnd(buff.Duration);
        EndBuff(buff, buffable);
    }
    
    private async UniTask WaitForBuffEnd(float duration) => await UniTask.WaitForSeconds(duration);

    private void EndBuff(Buff buff, IBuffable buffable) => buffable.EndBuff(buff);
}