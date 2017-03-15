using UnityEngine;
using System.Collections;

public class NX_Color {

	#region === 字段 === 
	
	#endregion
	
	#region === 属性 === 
	
	#endregion
	
	#region === 方法 === 
	
	#region === Unity默认函数 === 
	
	#endregion
	
	#region === 公有函数 === 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="factor">混合白色的程度，默认为0</param>
    /// <returns></returns>
    public Color GetRandomColor(float factor = 0)
    {
        Color newColor = new Color();
        int temp = Random.Range(0, 6);
        switch (temp)
        {
            case 0:
                newColor = Color.red;
                break;
            case 1:
                newColor = Color.green;
                break;
            case 2:
                newColor = Color.blue;
                break;
            case 3:
                newColor = new Color(1, 1, 0, 1);
                break;
            case 4:
                newColor = new Color(1, 0, 1, 1);
                break;
            case 5:
                newColor = new Color(0, 1, 1, 1);
                break;
        }
        newColor += (Color.white*factor);
        return newColor;
    }

    #endregion

    #region === 私有函数 === 

    #endregion

    #endregion
}
