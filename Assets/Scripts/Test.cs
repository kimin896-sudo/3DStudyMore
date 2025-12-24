using UnityEngine;

public class Test : MonoBehaviour
{
    LayerMask msk; // 
    void Start()
    {
        msk = LayerMask.GetMask("Monster") | LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ÇöÀç Âï°íÀÖ´Â È­¸éÀ» ¿ùµå ÁÂÇ¥·Î ¹Ù²ãÁÜ
            /*       Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.nearClipPlane)); //
                   Vector3 dir = mousePosition - Camera.main.transform.position;
                   dir = dir.normalized;*/

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 2);


            // ÀÌ°Ô °¡Àå ºü¸§ 
            int layerMask = (1 << Define.CLICK_LAYER);
           
            // (1 << 8) | (1 << 9)

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, msk))
            {
                Loger.Log($"raycast camera{hit.collider.gameObject.name}");
            }

            /*    Debug.DrawRay(Camera.main.transform.position, dir * 1000, Color.red, 100);

                if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit))
                {
                    Loger.Log($"raycast camera{hit.collider.gameObject.name}");
                }*/
        }


        /*       Debug.DrawRay(transform.position + Vector3.up, transform.forward*10,Color.red);

               RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.forward, 10);

               foreach(var hit in hits)
               {
                   Loger.Log($"{hit.collider.gameObject.name}! ºÎµúÈû");
               }*/
        /*  if(Physics.Raycast(transform.position + Vector3.up,Vector3.forward,out RaycastHit hit, 10))
          {
              Loger.Log($"{hit.collider.gameObject.name}! ºÎµúÈû");
          }*/



    }
}
