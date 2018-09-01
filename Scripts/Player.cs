using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //                 **************** Variables **************************************

     Rigidbody2D MyRigidbody;
    //Création d'une variable, type physique, appelé MyRigidbody (crée pour contenir le physique du jeu)

    Animator MyAnimator;
    
    public float MouvementSpeed;
    //Création d'une variable, type single, appelé MouvementSpeed, crée pour contenir le multiple appliqué à la vitesse(public mise pour le voir dans unity menu)

    bool FacingRight;
    //Création d'une variable, true or false, appelé FacingRight (crée pour savoir si le sprite est en direction droite)

    bool Attack;

    bool Slide;

    //                  **************** Use this for initialization *******************

    void Start () {

        MyRigidbody = GetComponent<Rigidbody2D>();
        //Prend le Rigidbody du joueur et le met dans une variable pour qu'on le manipule

        MyAnimator = GetComponent<Animator>();

        MouvementSpeed = 10;

        FacingRight = true;
	}
	
    // ******************************* Update is called per frame *****************************************

    void Update()
        //Pour vérifier les inputs le plus vite possible en allant fps de l'ordinateur
    {
        HandleInput();
    }

	//                  **************** Update is called per FixedUpdate (used to not relie updates per frame which change gameplay speed depending on hardware) ***************

	void FixedUpdate()
    {

        float horizontal = (Input.GetAxis("Horizontal"));
        // Prend les inputs et les met dans la variable, type single, appelée horizontal
        
        HandleMouvement(horizontal);
        //horizontal variable peut maintenant être utilisé dans le sub routine de HandleMouvement (horizontal est mise dedant)

        Flip(horizontal);

        HandleAttacks();

        ResetValues();

	}

    //                  **************** Sub-routines *********************************

     void HandleMouvement(float horizontal)
        //le type de variable horizontal est maintenant identifié pour ce sub routine car ce n'était pas identifier à initialization
    {
        if (!MyAnimator.GetBool("Slide") && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation"))
            
        {
         MyRigidbody.velocity = new Vector2(horizontal*MouvementSpeed, MyRigidbody.velocity.y);
        //Applique la valeur du input horizontal (donner du clavier) et du position y (qui est 0) sur la physique (MyRigidbody) qui controlle le mouvement du sprite, fait a chaque mise à jour
        }

        if (Slide && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SlideAnimation"))
        {
            MyAnimator.SetBool("Slide", true);
        }
        else if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SlideAnimation"))
        {
            MyAnimator.SetBool("Slide", false);
        }


        MyAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
        //Sa donne le float appelé Speed (dans unity) la valeur absolut de la variable horizontal qui s'occupe du mouvement pour justifier un animation running

    }

    void HandleAttacks()
        //Vérifier a chaque fixedupdate si trigger attack était fait pour faire l'animation et arrêter le mouvement vélocité
    {
        if (Attack && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation"))
        {  
            MyAnimator.SetTrigger("Attack");
            MyRigidbody.velocity = Vector2.zero;
        }
    } 

    void HandleInput()

    {
        if (Input.GetKeyDown(KeyCode.Z))
            
        {
            Attack = true;
        }

        if (Input.GetKeyDown(KeyCode.X))

        {
            Slide = true;
        }
    }

    void Flip(float horizontal)
    //   && = and    ! = negative    || = or    {} = then
    {
        if (!this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("AttackAnimation") && !this.MyAnimator.GetCurrentAnimatorStateInfo(0).IsTag("SlideAnimation"))
        {
            if (horizontal > 0 && !FacingRight || horizontal < 0 && FacingRight)
            {
                FacingRight = !FacingRight;

                Vector3 TheScale = transform.localScale;
                TheScale.x *= -1; //ask teacher if this is a syntax or why it is possible !!!!!!!!!!!!
                transform.localScale = TheScale;
            }
        }
    }

    void ResetValues()

    {
        Attack = false;
        Slide = false;
    }

}