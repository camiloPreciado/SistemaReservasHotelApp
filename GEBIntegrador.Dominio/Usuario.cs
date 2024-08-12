using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class usuario
{
    public int n_id { get; set; }

    public int? n_id_persona { get; set; }

    public int? n_id_perfil { get; set; }

    public string? v_correo { get; set; }

    public string? v_usuario { get; set; }

    public string? v_clave { get; set; }

    /// <summary>
    /// Se asigna subestación cuando es colaborador
    /// </summary>
    //public int? n_id_subestacion { get; set; }

    /// <summary>
    /// Se asigna contrato cuando es contratista
    /// </summary>
    public int? n_id_contrato { get; set; }

    public int? n_estado { get; set; }

    public int n_is_delete { get; set; }

   // public virtual ICollection<contrato_autorizadore> contrato_autorizadores { get; set; } = new List<contrato_autorizadore>();

 //   public virtual ICollection<contrato> contraton_id_usuario_actualizaNavigations { get; set; } = new List<contrato>();

 //   public virtual ICollection<contrato> contraton_id_usuario_creaNavigations { get; set; } = new List<contrato>();

 //   public virtual contrato? n_id_contratoNavigation { get; set; }

    public virtual perfile? n_id_perfilNavigation { get; set; }

    public virtual persona? n_id_personaNavigation { get; set; }

    //public virtual ICollection<regional> regionaln_id_usuario_actualizaNavigations { get; set; } = new List<regional>();

    //public virtual ICollection<regional> regionaln_id_usuario_creaNavigations { get; set; } = new List<regional>();

    //public virtual ICollection<regional> regionaln_id_usuario_sstNavigations { get; set; } = new List<regional>();

    public virtual ICollection<reserva> reservas { get; set; } = new List<reserva>();

  //  public virtual ICollection<sede_area> sede_areas { get; set; } = new List<sede_area>();

    //public virtual ICollection<solicitud_area> solicitud_areas { get; set; } = new List<solicitud_area>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_actuNavigations { get; set; } = new List<solicitud_cabecera>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_aprob1Navigations { get; set; } = new List<solicitud_cabecera>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_aprob2Navigations { get; set; } = new List<solicitud_cabecera>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_aprob3Navigations { get; set; } = new List<solicitud_cabecera>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_aprob4Navigations { get; set; } = new List<solicitud_cabecera>();

    //public virtual ICollection<solicitud_cabecera> solicitud_cabeceran_id_usuario_regiNavigations { get; set; } = new List<solicitud_cabecera>();
}
