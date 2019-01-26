
/* Copyright (C) Fernando Holguin Weber, and Studio Libeccio - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential.
 * Written by Fernando Holguin <contact@fernhw.com>, January 2017
 * 
 * UNDER NO CIRCUMSTANCES IS Fernando Holguin Weber, OR STUDIO LIBECCIO, ITS PROGRAM DEVELOPERS OR SUPPLIERS LIABLE FOR ANY OF THE FOLLOWING, EVEN IF INFORMED OF THEIR POSSIBILITY: 
 * LOSS OF, OR DAMAGE TO, DATA;
 * DIRECT, SPECIAL, INCIDENTAL, OR INDIRECT DAMAGES, OR FOR ANY ECONOMIC CONSEQUENTIAL DAMAGES; OR
 * LOST PROFITS, BUSINESS, REVENUE, GOODWILL, OR ANTICIPATED SAVINGS.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllOfClass  {

	public static List <SelectedClass> PickAllOf <SelectedClass> () where SelectedClass : class
    {
        MonoBehaviour[] monoBehaviours = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
        List<SelectedClass> list = new List<SelectedClass>();

        foreach (MonoBehaviour behaviour in monoBehaviours)
        {
            SelectedClass component = behaviour.GetComponent(typeof(SelectedClass)) as SelectedClass;

            if (component != null)
            {
                list.Add(component);
            }
        }
        return list;
    }
}
